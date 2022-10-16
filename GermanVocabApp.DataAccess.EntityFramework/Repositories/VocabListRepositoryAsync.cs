﻿using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.EntityFramework.Conversion;
using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.EntityFramework.ModificationExtensions;
using GermanVocabApp.DataAccess.EntityFramework.Projection;
using GermanVocabApp.DataAccess.Shared;
using GermanVocabApp.DataAccess.Shared.Abstractions;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Microsoft.EntityFrameworkCore;
using Osiris.Utilities.Collections.Generic;

namespace GermanVocabApp.DataAccess.EntityFramework.Repositories;

public class VocabListRepositoryAsync : RepositoryBase, IVocabListRepositoryAsync
{
    public VocabListRepositoryAsync(VocabListDbContext context) : base(context)
    {

    }

    public async Task<VocabListDto?> Get(Guid id)
    {
        IQueryable<VocabList> query = Context.VocablLists
                                              .AsNoTracking()
                                              .Where(vl => vl.Id == id
                                                        && vl.DeletedDate == null)
                                              .ProjectToListWithItems(id);
        VocabList entity = await query.SingleOrDefaultAsync();

        if (entity == null)
        {
            return null;
        }

        if (entity.ListItems == null)
        {
            throw new NullReferenceException($"VocabList entity ListItems property returned null in {nameof(VocabListRepositoryAsync)}.{nameof(Get)}." +
                                             $"\n\nCheck Entity Framework is behaving as expected.");
        }
        return entity.ToDto();
    }

    public async Task<IEnumerable<VocabListInfoDto>> GetVocabListInfos()
    {
        IEnumerable<VocabListInfoDto> listInfoDtos;
        listInfoDtos = await Context.VocablLists
                                    .AsNoTracking()
                                    .Where(vl => vl.DeletedDate == null)
                                    .ProjectToInfoDto()
                                    .ToArrayAsync();
        return listInfoDtos;
    }

    public async Task<VocabListDto> Add(VocabListDto dto)
    {
        VocabList entity = dto.ToEntityWithListItems();
        
        Context.Add(entity);
        await Context.SaveChangesAsync();

        VocabListDto retrievalDto = entity.ToDto();
        return retrievalDto;
    }

    public async Task Update(VocabListDto dto)
    {
        DateTime currentTimestamp = DateTime.UtcNow;

        if (!dto.Id.HasValue)
        {
            throw new UnexpectedNullIdException("Expect non-null vocab list ID when updating records.");
        }

        Guid listId = dto.Id.Value;

        VocabList existingList = await Context.VocablLists
                                               .TryGetFirstActiveWithId(listId);

        dto.CopyListDetails(existingList, currentTimestamp);

        IEnumerable<VocabListItem> existingListItems = existingList.ListItems;
        bool allItemsDeleted = CheckDeleteAllListItems(existingListItems,
                                                       dto.ListItems);
        if (allItemsDeleted)
        {
            await Context.SaveChangesAsync();
            return;
        }

        SoftDeletedRemovedListItems(dto, currentTimestamp, existingListItems);

        Dictionary<Guid, VocabListItem> nonDeletedListItemEntities;
        nonDeletedListItemEntities = existingListItems.Where(li => li.DeletedDate == null)
                                                      .ToDictionary(li => li.Id);
        AddOrUpdateListItems(dto, currentTimestamp, nonDeletedListItemEntities);

        await Context.SaveChangesAsync();
    }

    public async Task<bool> HardDelete(Guid id)
    {
        VocabList list;
        try
        {
            list = await Context.VocablLists
                                 .Include(vl => vl.ListItems)
                                 .FirstAsync(vl => vl.Id == id);
        }
        catch (InvalidOperationException)
        {
            return false;
        }

        if (list.ListItems.Any())
        {
            HardDeleteRangeWhere(list.ListItems, li => true);
        }
        Context.VocablLists.Remove(list);

        await Context.SaveChangesAsync();
        return true;
    }

    private void AddOrUpdateListItems(VocabListDto dto, DateTime currentTimestamp, Dictionary<Guid, VocabListItem> nonDeletedListItemEntities)
    {
        dto.ListItems.ForEach(item =>
        {
            VocabListItem newListItem = TryCreateItemNewItem(item, currentTimestamp);
            if (newListItem != null)
            {
                Context.Add(newListItem);
                return;
            }

            TryUpdateListItem(item, nonDeletedListItemEntities, currentTimestamp);
        });
    }

    private bool CheckDeleteAllListItems(IEnumerable<VocabListItem> existingListItems,
        IEnumerable<VocabListItemDto> updatedListItems)
    {
        bool areAllListItemsDeleted = !updatedListItems.Any() && existingListItems.Any();
        if (areAllListItemsDeleted)
        {
            Context.RemoveRange(existingListItems);
            return true;
        }
        return false;
    }

    private void SoftDeletedRemovedListItems(VocabListDto updateDto, DateTime currentTimestamp, IEnumerable<VocabListItem> existingListItems)
    {
        Dictionary<Guid, VocabListItemDto> updatedListItems;
        updatedListItems = updateDto.ListItems
                                    .Where(li => li.Id.HasValue)
                                    .ToDictionary(li => li.Id.Value);
        SoftDeleteRangeWhere(existingListItems, item => !updatedListItems.ContainsKey(item.Id));
    }

    private VocabListItem? TryCreateItemNewItem(VocabListItemDto updatedItemDto, DateTime transactionTimestamp)
    {
        if (updatedItemDto.Id.HasValue)
        {
            return null;
        }
        VocabListItem newListItem;
        try
        {
            newListItem = updatedItemDto.ToEntity();
        }
        catch (UnexpectedIdException e)
        {
            throw e;
        }
        return newListItem;
    }

    private void TryUpdateListItem(VocabListItemDto updatedItem, Dictionary<Guid, VocabListItem> entities,
        DateTime transactionTimestamp)
    {
        if (!updatedItem.Id.HasValue)
        {
            throw new InvalidOperationException("Cannot update list item with null ID value.");
        }

        Guid listItemId = updatedItem.Id.Value;
        if (!entities.ContainsKey(listItemId))
        {
            throw new EntityNotFoundException($"Vocab list item with ID {listItemId} not found in "
                                            + $"Vocab List with ID {updatedItem.VocabListId}.");
        }
        VocabListItem existingListItem = entities[listItemId];
        updatedItem.CopyTo(existingListItem);
    }
}
