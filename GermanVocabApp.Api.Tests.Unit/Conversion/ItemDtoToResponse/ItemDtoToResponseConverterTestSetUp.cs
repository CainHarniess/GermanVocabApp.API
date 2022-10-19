using AutoFixture;
using GermanVocabApp.Api.VocabLists.Conversion.Items;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.Tests.Unit.Conversion.ItemDtoToResponse;

public abstract class ItemDtoToResponseConverterTestSetUp
{
    protected Fixture _fixture;
    protected VocabListItemDto _dto;
    protected ItemDtoToResponseConverter _converter;

    protected ItemDtoToResponseConverterTestSetUp()
    {
        _fixture = new Fixture();
        _dto = _fixture.Create<VocabListItemDto>();

        _converter = new();
    }
}
