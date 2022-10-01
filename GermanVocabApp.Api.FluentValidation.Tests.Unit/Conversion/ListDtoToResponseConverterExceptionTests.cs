using GermanVocabApp.Api.VocabLists.Conversion;
using GermanVocabApp.Core.Exceptions;

namespace GermanVocabApp.Api.FluentValidation.Tests.Unit.Conversion;

public class ListDtoToResponseConverterExceptionTests : ListDtoToResponseConverterTests
{
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
