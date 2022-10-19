using GermanVocabApp.Api.VocabLists.Conversion.Lists;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Moq;

namespace GermanVocabApp.Api.FluentValidation.Tests.Unit.Conversion.ListRequestToDto;

public class CreateListRequestToDtoConverterValueTests : ListRequestToDtoConverterSetup
{
    private readonly Mock<IConverter<ItemRequest[], VocabListItemDto[]>> _mockItemsConverter;
    private readonly CreateListRequestToDtoConverter _converter;

    public CreateListRequestToDtoConverterValueTests() : base()
    {
        _mockItemsConverter = new Mock<IConverter<ItemRequest[], VocabListItemDto[]>>();
        _converter = new CreateListRequestToDtoConverter(_mockItemsConverter.Object);
    }

    [Fact]
    public void Convert_ShouldThrowException_IfIdNotNull()
    {
        Assert.Throws<UnexpectedIdException>(() => _converter.Convert(_request));
    }

    [Fact]
    public void Convert_ShouldCopyValues()
    {
        _request.Id = null;
        var result = _converter.Convert(_request);
        Assert.Equal(result.Name, _request.Name);
        Assert.Equal(result.Description, _request.Description);
    }
}
