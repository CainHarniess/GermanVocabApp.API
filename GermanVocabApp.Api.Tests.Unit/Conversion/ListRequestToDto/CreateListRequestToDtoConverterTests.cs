using GermanVocabApp.Api.VocabLists.Conversion.Lists;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.Shared.Vocab.Models;
using Moq;

namespace GermanVocabApp.Api.Tests.Unit.Conversion;

public class CreateListRequestToDtoConverterTests : ListRequestToDtoConverterSetup
{
    private readonly Mock<IConverter<ItemRequest[], VocabListItemDto[]>> _mockItemConverter;
    private readonly CreateListRequestToDtoConverter _converter;

    public CreateListRequestToDtoConverterTests() : base()
    {
        _mockItemConverter = new Mock<IConverter<ItemRequest[], VocabListItemDto[]>>();
        _converter = new CreateListRequestToDtoConverter(_mockItemConverter.Object);
        _request.Id = null;
    }

    [Fact]
    public void Convert_ShouldCopyValues()
    {
        var result = _converter.Convert(_request);
        Assert.Equal(result.Name, _request.Name);
        Assert.Equal(result.Description, _request.Description);
    }

    [Fact]
    public void Convert_ShouldCallItemConverter_WithCorrectValue()
    {
        _mockItemConverter.Setup(ic => ic.Convert(_request.ListItems.ToArray()));
        _converter.Convert(_request);
        _mockItemConverter.Verify(m => m.Convert(_request.ListItems.ToArray()), Times.Once);
    }

    [Fact]
    public void Convert_ShouldThrowException_WhenIdNotNull()
    {
        _request.Id = Guid.NewGuid();
        Assert.Throws<UnexpectedIdException>(() => _converter.Convert(_request));
    }

    [Fact]
    public void Convert_ShouldNotThrowException_WhenIdNull()
    {
        Exception e = Record.Exception(() => _converter.Convert(_request));
        Assert.Null(e);
    }
}
