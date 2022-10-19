using GermanVocabApp.Api.VocabLists.Models;

namespace GermanVocabApp.Api.Tests.Unit.Conversion.ListDtoToResponse;

public class ListDtoToResponseConverterValueTests : ListDtoToResponseConverterTests
{
    [Fact]
    public void Convert_ShouldCopyValues()
    {
        ListResponse response = _converter.Convert(_dto);
        Assert.Equal(_dto.Id, response.Id);
        Assert.Equal(_dto.Name, response.Name);
        Assert.Equal(_dto.Description, response.Description);
    }
}
