using AutoFixture;
using GermanVocabApp.Api.VocabLists.Conversion.Lists;
using GermanVocabApp.DataAccess.Shared.Vocab.Models;

namespace GermanVocabApp.Api.Tests.Unit.Conversion;

public class ListInfoDtoToResponseCoverterTests
{
    private VocabListInfoDto _dto;
    private ListInfoDtoToResponseCoverter _converter;

    public ListInfoDtoToResponseCoverterTests()
    {
        var fixture = new Fixture();
        _dto = fixture.Create<VocabListInfoDto>();

        _converter = new();
    }

    [Fact]
    public void Convert_ShouldCopyValues()
    {
        var response = _converter.Convert(_dto);

        Assert.Equal(_dto.Id, response.Id);
        Assert.Equal(_dto.Name, response.Name);
        Assert.Equal(_dto.Description, response.Description);
    }
}
