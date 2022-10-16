using GermanVocabApp.DataAccess.Shared.DataTransfer;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.DataAccess.Models.Builders;

public partial class VocabListItemDtoBuilder : EntityDtoBuilder<VocabListItemDto, VocabListItemDtoBuilder>
{
    public VocabListItemDtoBuilder Kettle(Guid listId)
    {
        return Kettle().WithListId(listId);
    }

    public VocabListItemDtoBuilder Kettle()
    {
        return WithWordType(WordType.Noun)
              .WithWeakMasculineNoun(false)
              .WithGender(Gender.Masculine)
              .WithGerman("Wasserkocher")
              .WithPlural("Wasserköcher")
              .WithEnglish("kettle")
              .WithFixedPlurality(FixedPlurality.None);
    }

    public VocabListItemDtoBuilder Knife()
    {
        return WithWordType(WordType.Noun)
              .WithWeakMasculineNoun(false)
              .WithGender(Gender.Masculine)
              .WithGerman("Messer")
              .WithPlural("Messer")
              .WithEnglish("knife")
              .WithFixedPlurality(FixedPlurality.None);
    }

    public VocabListItemDtoBuilder ToCook(Guid listId)
    {
        return ToCook().WithListId(listId);
    }

    public VocabListItemDtoBuilder ToCook()
    {
        return WithWordType(WordType.Verb)
              .WithSeparability(Separability.None)
              .WithTransitivity(Transitivity.Both)
              .WithHaben()
              .WithGerman("kochen")
              .WithEnglish("to cook");
    }

    public VocabListItemDtoBuilder ToChop()
    {
        return WithWordType(WordType.Verb)
              .WithSeparability(Separability.None)
              .WithTransitivity(Transitivity.Transitive)
              .WithThirdPersonImperfect("schnitt")
              .WithHaben()
              .WithPerfect("geschnitten")
              .WithGerman("schneiden")
              .WithEnglish("to chop");
    }

    public VocabListItemDtoBuilder Spicy(Guid listId)
    {
        return Spicy().WithListId(listId);
    }

    public VocabListItemDtoBuilder Spicy()
    {
        return WithWordType(WordType.Adjective)
              .WithGerman("scharf")
              .WithEnglish("spicy");
    }

    public VocabListItemDtoBuilder Saucily(Guid listId)
    {
        return Spicy().WithListId(listId);
    }

    public VocabListItemDtoBuilder Saucily()
    {
        return WithWordType(WordType.Adverb)
              .WithGerman("aufreizend")
              .WithEnglish("saucily");
    }
}
