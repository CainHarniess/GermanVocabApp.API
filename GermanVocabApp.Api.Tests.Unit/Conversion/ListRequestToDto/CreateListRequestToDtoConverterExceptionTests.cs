using GermanVocabApp.Core.Exceptions;

namespace GermanVocabApp.Api.Tests.Unit.Conversion.ListDtoToResponse;

public class CreateListRequestToDtoConverterExceptionTests : ListDtoToResponseConverterTests
{
    [Fact]
    public void Convert_ShouldThrowException_WhenIdNull()
    {
        _dto.Id = null;
        Assert.Throws<UnexpectedNullIdException>(() => _converter.Convert(_dto));
    }

    [Fact]
    public void Convert_ShouldNotThrowException_WhenIdNotNull()
    {
        Exception e = Record.Exception(() => _converter.Convert(_dto));
        Assert.Null(e);
    }
}
