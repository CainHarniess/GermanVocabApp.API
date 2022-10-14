using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.EntityFramework.Conversion;
using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.EntityFramework.ModificationExtensions;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Osiris.Utilities.Collections.Generic;

namespace GermanVocabApp.DataAccess.EntityFramework.Repositories;

public class ItemRepositoryAsync : RepositoryBase
{
    public ItemRepositoryAsync(VocabListDbContext context)
        : base(context)
    {

    }

    public async Task AddRangeAsync(VocabListItemDto[] dtos, Guid listId, DateTime transactionTimeStamp)
    {
        VocabListItem[] entities;
        entities = dtos.ToEntities(transactionTimeStamp, listId);
        await Context.AddRangeAsync(entities);
    }

    public void Update(VocabList list, VocabListItemDto[] updateItems, DateTime transactionTimeStamp)
    {
        VocabListItem[] existingListItems = list.ListItems.ToArray();
        bool areAllListItemsDeleted = !updateItems.Any() && existingListItems.Any();

        if (areAllListItemsDeleted)
        {
            existingListItems.SoftDeleteAll(transactionTimeStamp);
        }

        SoftDeletedRemovedListItems(updateItems, transactionTimeStamp, existingListItems);

        Dictionary<Guid, VocabListItem> nonDeletedListItemEntities;
        nonDeletedListItemEntities = existingListItems.Where(li => li.DeletedDate == null)
                                                      .ToDictionary(li => li.Id);
        AddOrUpdateListItems(updateItems, list.Id, transactionTimeStamp, nonDeletedListItemEntities);
        return;
    }

    private static void SoftDeletedRemovedListItems(VocabListItemDto[] updateItems, DateTime currentTimestamp,
                                                    VocabListItem[] existingListItems)
    {
        Dictionary<Guid, VocabListItemDto> updatedListItemsDict;
        updatedListItemsDict = updateItems.Where(li => li.Id.HasValue)
                                          .ToDictionary(li => li.Id.Value);

        existingListItems.SoftDeleteWhere(item => !updatedListItemsDict.ContainsKey(item.Id), currentTimestamp);
    }

    private void AddOrUpdateListItems(VocabListItemDto[] updateItems, Guid listId, DateTime currentTimestamp,
                                      Dictionary<Guid, VocabListItem> nonDeletedListItemEntities)
    {
        updateItems.ForEach(item =>
        {
            VocabListItem newListItem = TryValidateAndConvertNewItem(item, listId, currentTimestamp);
            if (newListItem != null)
            {
                Context.Add(newListItem);
                return;
            }

            //TryUpdateListItem(item, nonDeletedListItemEntities, currentTimestamp);
        });
    }

    private static VocabListItem? TryValidateAndConvertNewItem(VocabListItemDto updatedItemDto, Guid listId, DateTime transactionTimestamp)
    {
        if (updatedItemDto.Id.HasValue)
        {
            return null;
        }
        VocabListItem newListItem;
        try
        {
            newListItem = updatedItemDto.ToEntity(transactionTimestamp);
        }
        catch (UnexpectedIdException e)
        {
            throw e;
        }
        newListItem.VocabListId = listId;
        return newListItem;
    }
}