namespace GermanVocabApp.Api.Tests.Unit.Conversion;

public class ItemDtoToResponseConverterValueTests : ItemDtoToResponseConverterTestSetUp
{
    [Fact]
    public void ToResponse_ShouldCopyValues()
    {
        var _response = _converter.Convert(_dto);

        Assert.Equal(_dto.Id, _response.Id);
        Assert.Equal(_dto.WordType, _response.WordType);
        Assert.Equal(_dto.IsWeakMasculineNoun, _response.IsWeakMasculineNoun);
        Assert.Equal(_dto.ReflexiveCase, _response.ReflexiveCase);
        Assert.Equal(_dto.Separability, _response.Separability);
        Assert.Equal(_dto.Transitivity, _response.Transitivity);
        Assert.Equal(_dto.ThirdPersonPresent, _response.ThirdPersonPresent);
        Assert.Equal(_dto.ThirdPersonImperfect, _response.ThirdPersonImperfect);
        Assert.Equal(_dto.AuxiliaryVerb, _response.AuxiliaryVerb);
        Assert.Equal(_dto.Perfect, _response.Perfect);
        Assert.Equal(_dto.Gender, _response.Gender);
        Assert.Equal(_dto.German, _response.German);
        Assert.Equal(_dto.Plural, _response.Plural);
        Assert.Equal(_dto.Preposition, _response.Preposition);
        Assert.Equal(_dto.PrepositionCase, _response.PrepositionCase);
        Assert.Equal(_dto.Comparative, _response.Comparative);
        Assert.Equal(_dto.Superlative, _response.Superlative);
        Assert.Equal(_dto.English, _response.English);
        Assert.Equal(_dto.VocabListId, _response.VocabListId);
        Assert.Equal(_dto.FixedPlurality, _response.FixedPlurality);
    }
}
