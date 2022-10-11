﻿using GermanVocabApp.DataAccess.EntityFramework.Models;

namespace GermanVocabApp.DataAccess.Models.Builders;

public partial class VocabListBuilder : EntityBuilder<VocabList, VocabListBuilder>
{
    private readonly VocabListItemBuilder _itemBuilder;

    public VocabListBuilder(VocabListItemBuilder itemBuilder)
    {
        _itemBuilder = itemBuilder;
    }

    public VocabListBuilder()
    {
        _itemBuilder = new VocabListItemBuilder();
    }

    public VocabListBuilder Default()
    {
        return this;
    }

    public VocabListBuilder Kitchen()
    {
        Guid listId = Guid.NewGuid();
        DateTime creationTimestamp = DateTime.UtcNow;

        VocabListItem[] items = new VocabListItem[]
        {
            _itemBuilder.Kettle(listId).WithCreatedDate(creationTimestamp).Build(),
            _itemBuilder.ToPlay(listId).WithCreatedDate(creationTimestamp).Build(),
            _itemBuilder.Spicy(listId).WithCreatedDate(creationTimestamp).Build(),
            _itemBuilder.Saucily(listId).WithCreatedDate(creationTimestamp).Build(),
        };

        return  WithId(listId)
               .WithName("Kitchen")
               .WithDescription("A culinary collection.")
               .WithListItems(items)
               .WithCreatedDate(creationTimestamp);
    }
}
