using AutoFixture;
using GermanVocabApp.Api.VocabLists.Models;

namespace GermanVocabApp.Api.FluentValidation.Tests.Unit.Conversion.ListRequestToDto;

public abstract class ListRequestToDtoConverterSetup
{
    protected readonly Fixture _fixture;
    protected ListRequest _request;

    protected ListRequestToDtoConverterSetup()
    {
        _fixture = new Fixture();
        _request = _fixture.Create<ListRequest>();
    }
}
