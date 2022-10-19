using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.DataAccess.Models.Builders;

public partial class VocabListItemBuilder : EntityBuilder<VocabListItem, VocabListItemBuilder>
{
    public VocabListItemBuilder Kettle(Guid listId)
    {
        return Kettle().WithListId(listId);
    }

    public VocabListItemBuilder Kettle()
    {
        return WithId(Guid.NewGuid())
              .WithWordType(WordType.Noun)
              .WithWeakMasculineNoun(false)
              .WithGender(Gender.Masculine)
              .WithGerman("Wasserkocher")
              .WithPlural("Wasserköcher")
              .WithEnglish("kettle")
              .WithFixedPlurality(FixedPlurality.None)
              .WithCreatedDate(DateTime.UtcNow);
    }

    public VocabListItemBuilder ToPlay(Guid listId)
    {
        return ToCook().WithListId(listId);
    }

    public VocabListItemBuilder ToCook()
    {
        return WithId(Guid.NewGuid())
              .WithWordType(WordType.Verb)
              .WithSeparability(Separability.None)
              .WithTransitivity(Transitivity.Both)
              .WithHaben()
              .WithGerman("kochen")
              .WithEnglish("to cook");
    }

    public VocabListItemBuilder Spicy(Guid listId)
    {
        return Spicy().WithListId(listId);
    }

    public VocabListItemBuilder Spicy()
    {
        return WithId(Guid.NewGuid())
              .WithWordType(WordType.Adjective)
              .WithGerman("scharf")
              .WithEnglish("spicy");
    }

    public VocabListItemBuilder Saucily(Guid listId)
    {
        return Spicy().WithListId(listId);
    }

    public VocabListItemBuilder Saucily()
    {
        return WithId(Guid.NewGuid())
              .WithWordType(WordType.Adverb)
              .WithGerman("aufreizend")
              .WithEnglish("saucily");
    }

    public VocabListItemBuilder BasicWithListId(Guid listId)
    {
        return WithId(Guid.NewGuid())
              .WithWordType(WordType.Noun)
              .WithEnglish("Test List Item")
              .WithGerman("Tester Listeintrag")
              .WithListId(listId);
    }
}
