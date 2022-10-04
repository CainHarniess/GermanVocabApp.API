using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.DataAccess.EntityFramework.Tests.Unit.Builders;

public class VocabListItemBuilder : EntityBuilder<VocabListItem>
{
    private Guid? _listId;
    private WordType? _wordType;
    private string? _english;
    private string? _german;

    public override VocabListItem Build()
    {
        var item = new VocabListItem();

        ApplyBaseValues(item);

        item.VocabListId = _listId ?? Guid.Empty;
        item.WordType = _wordType ?? WordType.Noun;
        item.English = _english ?? "Default English";
        item.German = _german ?? "Default Deutsch";

        return item;
    }

    public VocabListItemBuilder BasicWithListId(Guid listId)
    {
        return WithId(new Guid())
              .WithWordType(WordType.Noun)
              .WithEnglish("Test List Item")
              .WithGerman("Tester Listeintrag")
              .WithListId(listId);
    }

    public VocabListItemBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public VocabListItemBuilder WithListId(Guid listId)
    {
        _listId = listId;
        return this;
    }

    public VocabListItemBuilder WithWordType(WordType wordType)
    {
        _wordType = wordType;
        return this;
    }

    public VocabListItemBuilder WithEnglish(string english)
    {
        _english = english;
        return this;
    }

    public VocabListItemBuilder WithGerman(string german)
    {
        _german = german;
        return this;
    }
}
