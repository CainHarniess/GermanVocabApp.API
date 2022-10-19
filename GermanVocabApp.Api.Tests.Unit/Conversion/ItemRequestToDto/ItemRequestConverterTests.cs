using AutoFixture;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.Tests.Unit.Conversion;

public abstract class ItemRequestConverterTests
{
    protected readonly Fixture _fixture;
    protected readonly ItemRequest _request;
    protected VocabListItemDto _dto;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected ItemRequestConverterTests()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        _fixture = new Fixture();
        _request = _fixture.Create<ItemRequest>();
        _request.Id = null;
    }

    [Fact]
    public void ToCreationDto_ShouldCopyValues()
    {
        Assert.Equal(_request.WordType, _dto.WordType);
        Assert.Equal(_request.IsWeakMasculineNoun, _dto.IsWeakMasculineNoun);
        Assert.Equal(_request.ReflexiveCase, _dto.ReflexiveCase);
        Assert.Equal(_request.Separability, _dto.Separability);
        Assert.Equal(_request.Transitivity, _dto.Transitivity);
        Assert.Equal(_request.ThirdPersonPresent, _dto.ThirdPersonPresent);
        Assert.Equal(_request.ThirdPersonImperfect, _dto.ThirdPersonImperfect);
        Assert.Equal(_request.AuxiliaryVerb, _dto.AuxiliaryVerb);
        Assert.Equal(_request.Perfect, _dto.Perfect);
        Assert.Equal(_request.Gender, _dto.Gender);
        Assert.Equal(_request.German, _dto.German);
        Assert.Equal(_request.Plural, _dto.Plural);
        Assert.Equal(_request.Preposition, _dto.Preposition);
        Assert.Equal(_request.PrepositionCase, _dto.PrepositionCase);
        Assert.Equal(_request.Comparative, _dto.Comparative);
        Assert.Equal(_request.Superlative, _dto.Superlative);
        Assert.Equal(_request.English, _dto.English);
        Assert.Equal(_request.FixedPlurality, _dto.FixedPlurality);
    }
}