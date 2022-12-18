using GermanVocabApp.DataAccess.Shared.Core;
using GermanVocabApp.DataAccess.Shared.Vocab.Models;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.DataAccess.Models.Builders;

public partial class VocabListItemDtoBuilder : EntityDtoBuilder<VocabListItemDto, VocabListItemDtoBuilder>
{
    private WordType? _wordType;
    private bool? _isWeakMasculineNoun;
    private ReflexiveCase? _reflexiveCase;
    private Separability? _separability;
    private Transitivity? _transitivity;
    private string? _thirdPersonPresent;
    private string? _thirdPersonImperfect;
    private AuxiliaryVerb? _auxiliaryVerb;
    private string? _perfect;
    private Gender? _gender;
    private string _german = "Test German";
    private string? _plural;
    private string? _preposition;
    private Case? _prepositionCase;
    private string? _comparative;
    private string? _superlative;
    private string _english = "Test English";
    private Guid? _listId;
    private FixedPlurality? _fixedPlurality;

    protected override VocabListItemDtoBuilder Instance => this;

    public VocabListItemDtoBuilder WithWordType(WordType wordType)
    {
        _wordType = wordType;
        return Instance;
    }

    public VocabListItemDtoBuilder WithWeakMasculineNoun(bool isWeakMasculineNoun)
    {
        _isWeakMasculineNoun = isWeakMasculineNoun;
        return Instance;
    }

    public VocabListItemDtoBuilder WithSeparability(Separability separability)
    {
        _separability = separability;
        return Instance;
    }

    public VocabListItemDtoBuilder WithTransitivity(Transitivity transitivity)
    {
        _transitivity = transitivity;
        return Instance;
    }

    public VocabListItemDtoBuilder WithThirdPersonPresent(string thirdPersonPresent)
    {
        _thirdPersonPresent = thirdPersonPresent;
        return Instance;
    }

    public VocabListItemDtoBuilder WithThirdPersonImperfect(string thirdPersonImperfect)
    {
        _thirdPersonImperfect = thirdPersonImperfect;
        return Instance;
    }

    public VocabListItemDtoBuilder WithAuxiliaryVerb(AuxiliaryVerb auxiliaryVerb)
    {
        _auxiliaryVerb = auxiliaryVerb;
        return Instance;
    }

    public VocabListItemDtoBuilder WithHaben()
    {
        return WithAuxiliaryVerb(AuxiliaryVerb.Haben);
    }

    public VocabListItemDtoBuilder WithSein()
    {
        return WithAuxiliaryVerb(AuxiliaryVerb.Sein);
    }

    public VocabListItemDtoBuilder WithPerfect(string perfect)
    {
        _perfect = perfect;
        return Instance;
    }

    public VocabListItemDtoBuilder WithGender(Gender gender)
    {
        _gender = gender;
        return Instance;
    }

    public VocabListItemDtoBuilder WithGerman(string german)
    {
        _german = german;
        return Instance;
    }

    public VocabListItemDtoBuilder WithPlural(string _plural)
    {
        this._plural = _plural;
        return Instance;
    }

    public VocabListItemDtoBuilder WithComparative(string comparative)
    {
        _comparative = comparative;
        return Instance;
    }

    public VocabListItemDtoBuilder WithPreposition(string preposition)
    {
        _preposition = preposition;
        return Instance;
    }

    public VocabListItemDtoBuilder WithPrepositionCase(Case prepositionCase)
    {
        _prepositionCase = prepositionCase;
        return Instance;
    }

    public VocabListItemDtoBuilder WithSuperlative(string superlative)
    {
        _superlative = superlative;
        return Instance;
    }

    public VocabListItemDtoBuilder WithEnglish(string english)
    {
        _english = english;
        return Instance;
    }

    public VocabListItemDtoBuilder WithListId(Guid listId)
    {
        _listId = listId;
        return Instance;
    }

    public VocabListItemDtoBuilder WithFixedPlurality(FixedPlurality fixedPlurality)
    {
        _fixedPlurality = fixedPlurality;
        return Instance;
    }

    public VocabListItemDtoBuilder WithReflexiveCase(ReflexiveCase reflexiveCase)
    {
        _reflexiveCase = reflexiveCase;
        return Instance;
    }

    public override VocabListItemDto Build()
    {
        var item = new VocabListItemDto();
        ApplyValues(item);
        Clear();
        return item;
    }

    protected override void ApplyValues(VocabListItemDto item)
    {
        base.ApplyValues(item);
        item.WordType = _wordType ?? WordType.Noun;
        item.IsWeakMasculineNoun = _isWeakMasculineNoun;
        item.ReflexiveCase = _reflexiveCase;
        item.Separability = _separability;
        item.Transitivity = _transitivity;
        item.ThirdPersonPresent = _thirdPersonPresent;
        item.ThirdPersonImperfect = _thirdPersonImperfect;
        item.AuxiliaryVerb = _auxiliaryVerb;
        item.Perfect = _perfect;
        item.Gender = _gender;
        item.German = _german ?? "Default Deutsch";
        item.Plural = _plural;
        item.Preposition = _preposition;
        item.PrepositionCase = _prepositionCase;
        item.Comparative = _comparative;
        item.Superlative = _superlative;
        item.English = _english ?? "Default English";
        item.VocabListId = _listId;
        item.FixedPlurality = _fixedPlurality;
    }

    protected override void Clear()
    {
        _wordType = default;
        _isWeakMasculineNoun = default;
        _reflexiveCase = default;
        _separability = default;
        _transitivity = default;
        _thirdPersonPresent = default;
        _thirdPersonImperfect = default;
        _auxiliaryVerb = default;
        _perfect = default;
        _gender = default;
        _german = "Test German";
        _plural = default;
        _preposition = default;
        _prepositionCase = default;
        _comparative = default;
        _superlative = default;
        _english = "Test English";
        _listId = default;
        _fixedPlurality = default;
    }
}
