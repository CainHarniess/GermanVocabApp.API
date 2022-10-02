using GermanVocabApp.Core.Exceptions;

namespace GermanVocabApp.Api.FluentValidation.Tests.Unit.Conversion.ListDtoToResponse;

public class CreateListRequestToDtoConverterExceptionTests : ListDtoToResponseConverterTests
{
    [Fact]
    public void Convert_ShouldThrowException_WhenIdNull()
    {
        Assert.Throws<UnexpectedNullIdException>(() => _converter.Convert(_dto));
    }

    [Fact]
    public void Convert_ShouldNotThrowException_WhenIdNull()
    {
        _dto.Id = null;
        Exception e = Record.Exception(() => _converter.Convert(_dto));
        Assert.Null(e);
    }
}
