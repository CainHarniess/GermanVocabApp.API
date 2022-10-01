using AutoFixture;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.FluentValidation.Tests.Unit.Conversion;

public abstract class ItemRequestValueTests
{
    private Fixture _fixture;
    private ItemRequest _request;

    protected ItemRequestValueTests()
    {
        _fixture = new Fixture();
        _request = _fixture.Create<ItemRequest>();
    }

    protected Fixture Fixture => _fixture;
    protected ItemRequest Request => _request;
    protected VocabListItemDto Dto { get; set; }

    [Fact]
    public void ToCreationDto_ShouldCopyValues()
    {
        Assert.Equal(Request.WordType, Dto.WordType);
        Assert.Equal(Request.IsWeakMasculineNoun, Dto.IsWeakMasculineNoun);
        Assert.Equal(Request.ReflexiveCase, Dto.ReflexiveCase);
        Assert.Equal(Request.Separability, Dto.Separability);
        Assert.Equal(Request.Transitivity, Dto.Transitivity);
        Assert.Equal(Request.ThirdPersonPresent, Dto.ThirdPersonPresent);
        Assert.Equal(Request.ThirdPersonImperfect, Dto.ThirdPersonImperfect);
        Assert.Equal(Request.AuxiliaryVerb, Dto.AuxiliaryVerb);
        Assert.Equal(Request.Perfect, Dto.Perfect);
        Assert.Equal(Request.Gender, Dto.Gender);
        Assert.Equal(Request.German, Dto.German);
        Assert.Equal(Request.Plural, Dto.Plural);
        Assert.Equal(Request.Preposition, Dto.Preposition);
        Assert.Equal(Request.PrepositionCase, Dto.PrepositionCase);
        Assert.Equal(Request.Comparative, Dto.Comparative);
        Assert.Equal(Request.Superlative, Dto.Superlative);
        Assert.Equal(Request.English, Dto.English);
        Assert.Equal(Request.FixedPlurality, Dto.FixedPlurality);
    }
}