using AutoFixture;
using GermanVocabApp.Api.VocabLists.Conversion.Lists;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Moq;

namespace GermanVocabApp.Api.Tests.Unit.Conversion.ListDtoToResponse;

public class ListDtoToResponseConverterTests
{
    protected Fixture _fixture;
    protected VocabListDto _dto;
    protected Mock<IConverter<VocabListItemDto[], ItemResponse[]>> _mockItemConverter;
    protected ListDtoToResponseConverter _converter;

    public ListDtoToResponseConverterTests()
    {
        _fixture = new Fixture();
        _dto = _fixture.Create<VocabListDto>();

        _mockItemConverter = new Mock<IConverter<VocabListItemDto[], ItemResponse[]>>();
        _converter = new(_mockItemConverter.Object);
    }

    [Fact]
    public void Convert_ShouldCopyValues()
    {
        ListResponse response = _converter.Convert(_dto);
        Assert.Equal(_dto.Id, response.Id);
        Assert.Equal(_dto.Name, response.Name);
        Assert.Equal(_dto.Description, response.Description);
    }

    [Fact]
    public void Convert_ShouldCallItemConverter_WithCorrectValue()
    {
        _mockItemConverter.Setup(ic => ic.Convert(_dto.ListItems.ToArray()));
        _converter.Convert(_dto);
        _mockItemConverter.Verify(m => m.Convert(_dto.ListItems.ToArray()), Times.Once);
    }

    [Fact]
    public void Convert_ShouldThrowException_WhenDtoIdNull()
    {
        _dto.Id = null;
        Assert.Throws<UnexpectedNullIdException>(() => _converter.Convert(_dto));
    }

    [Fact]
    public void Convert_ShouldNotThrowException_WhenDtoIdNotNull()
    {
        Exception e = Record.Exception(() => _converter.Convert(_dto));
        Assert.Null(e);
    }
}
