using AutoFixture;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.FluentValidation.Tests.Unit.Conversion.ListRequestToDto;

public abstract class ListRequestToDtoConverterSetup
{
    protected readonly Fixture _fixture;
    protected ListRequest _request;
    protected VocabListDto _result;

    public ListRequestToDtoConverterSetup()
    {
        _fixture = new Fixture();
        _request = _fixture.Create<ListRequest>();
    }

    [Fact]
    public void Convert_ShouldCopyValues()
    {
        Assert.Equal(_result.Name, _request.Name);
        Assert.Equal(_result.Description, _request.Description);
    }
}
