using GermanVocabApp.Api.VocabLists.Conversion.Items;
using GermanVocabApp.Core.Exceptions;

namespace GermanVocabApp.Api.Tests.Unit.Conversion;

public class CreateItemRequestToDtoConverterTests : ItemRequestConverterTests
{
    private CreateItemRequestToDtoConverter _converter;
    public CreateItemRequestToDtoConverterTests() : base()
    {
        _converter = new CreateItemRequestToDtoConverter();
        _dto = _converter.Convert(_request);
    }

    [Fact]
    public void Convert_ShouldSetIdToNull()
    {
        Assert.Null(_dto.Id);
    }

    [Fact]
    public void Convert_ShouldSetListIdToNull()
    {
        Assert.Null(_dto.VocabListId);
    }

    [Fact]
    public void Convert_ShouldThrowException_WhenDtoIdNotNull()
    {
        _request.Id = new Guid();
        Assert.Throws<UnexpectedNullIdException>(() => _converter.Convert(_request));
    }

    [Fact]
    public void Convert_ShouldNotThrowException_WhenDtoIdNull()
    {
        Exception e = Record.Exception(() => _converter.Convert(_request));
        Assert.Null(e);
    }
}
