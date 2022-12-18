using GermanVocabApp.DataAccess.EntityFramework.Core;
using GermanVocabApp.DataAccess.EntityFramework.Vocab.Models;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.DataAccess.Models.Builders;

public partial class VocabListItemBuilder : EntityBuilder<VocabListItem, VocabListItemBuilder>
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
    private string _german;
    private string? _plural;
    private string? _preposition;
    private Case? _prepositionCase;
    private string? _comparative;
    private string? _superlative;
    private string _english;
    private Guid? _listId;
    private VocabList? _list;
    private FixedPlurality? _fixedPlurality;

    protected override VocabListItemBuilder Instance => this;
    
    public VocabListItemBuilder WithWordType(WordType wordType)
    {
        _wordType = wordType;
        return Instance;
    }

    public VocabListItemBuilder WithWeakMasculineNoun(bool isWeakMasculineNoun)
    {
        _isWeakMasculineNoun = isWeakMasculineNoun;
        return Instance;
    }

    public VocabListItemBuilder WithSeparability(Separability separability)
    {
        _separability = separability;
        return Instance;
    }

    public VocabListItemBuilder WithTransitivity(Transitivity transitivity)
    {
        _transitivity = transitivity;
        return Instance;
    }

    public VocabListItemBuilder WithThirdPersonPresent(string thirdPersonPresent)
    {
        _thirdPersonPresent = thirdPersonPresent;
        return Instance;
    }

    public VocabListItemBuilder WithThirdPersonImperfect(string thirdPersonImperfect)
    {
        _thirdPersonImperfect = thirdPersonImperfect;
        return Instance;
    }

    public VocabListItemBuilder WithAuxiliaryVerb(AuxiliaryVerb auxiliaryVerb)
    {
        _auxiliaryVerb = auxiliaryVerb;
        return Instance;
    }

    public VocabListItemBuilder WithHaben()
    {
        return WithAuxiliaryVerb(AuxiliaryVerb.Haben);
    }

    public VocabListItemBuilder WithSein()
    {
        return WithAuxiliaryVerb(AuxiliaryVerb.Sein);
    }

    public VocabListItemBuilder WithPerfect(string perfect)
    {
        _perfect = perfect;
        return Instance;
    }

    public VocabListItemBuilder WithGender(Gender gender)
    {
        _gender = gender;
        return Instance;
    }

    public VocabListItemBuilder WithGerman(string german)
    {
        _german = german;
        return Instance;
    }

    public VocabListItemBuilder WithPlural(string _plural)
    {
        this._plural = _plural;
        return Instance;
    }

    public VocabListItemBuilder WithComparative(string comparative)
    {
        _comparative = comparative;
        return Instance;
    }

    public VocabListItemBuilder WithPreposition(string preposition)
    {
        _preposition = preposition;
        return Instance;
    }

    public VocabListItemBuilder WithPrepositionCase(Case prepositionCase)
    {
        _prepositionCase = prepositionCase;
        return Instance;
    }

    public VocabListItemBuilder WithSuperlative(string superlative)
    {
        _superlative = superlative;
        return Instance;
    }

    public VocabListItemBuilder WithEnglish(string english)
    {
        _english = english;
        return Instance;
    }

    public VocabListItemBuilder WithListId(Guid listId)
    {
        _listId = listId;
        return Instance;
    }

    public VocabListItemBuilder WithFixedPlurality(FixedPlurality fixedPlurality)
    {
        _fixedPlurality = fixedPlurality;
        return Instance;
    }
    
    public VocabListItemBuilder WithReflexiveCase(ReflexiveCase reflexiveCase)
    {
        _reflexiveCase = reflexiveCase;
        return Instance;
    }

    public override VocabListItem Build()
    {
        var item = new VocabListItem();
        ApplyValues(item);
        Clear();
        return item;
    }

    protected override void ApplyValues(VocabListItem item)
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
        item.VocabListId = _listId ?? Guid.NewGuid();
        item.VocabList = _list;
        item.FixedPlurality = _fixedPlurality;
    }

    protected override void Clear()
    {
        base.Clear();
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
        _german = default;
        _plural = default;
        _preposition = default;
        _prepositionCase = default;
        _comparative = default;
        _superlative = default;
        _english = default;
        _listId = default;
        _list = default;
        _fixedPlurality = default;
}
}
