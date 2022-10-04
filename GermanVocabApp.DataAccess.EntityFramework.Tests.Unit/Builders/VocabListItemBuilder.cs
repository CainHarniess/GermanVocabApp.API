using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.DataAccess.EntityFramework.Tests.Unit.Builders;

public partial class VocabListItemBuilder : BuilderBase<VocabListItem>
{
    public VocabListItemBuilder BasicWithListId(Guid listId)
    {
        return WithId(new Guid())
              .WithWordType(WordType.Noun)
              .WithEnglish("Test List Item")
              .WithGerman("Tester Listeintrag")
              .WithVocabListId(listId);
    }
}
