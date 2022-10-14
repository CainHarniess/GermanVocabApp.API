using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.EntityFramework.Conversion;
using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.EntityFramework.ModificationExtensions;
using GermanVocabApp.DataAccess.EntityFramework.Projection;
using GermanVocabApp.DataAccess.Shared;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Microsoft.EntityFrameworkCore;
using Osiris.Utilities.Collections.Generic;

namespace GermanVocabApp.DataAccess.EntityFramework.Repositories;

public class VocabListRepositoryAsync : IVocabListRepositoryAsync
{
    private readonly VocabListDbContext _context;

    public VocabListRepositoryAsync(VocabListDbContext context)
    {
        _context = context;
    }

    public async Task<VocabListDto?> Get(Guid id)
    {
        IQueryable<VocabList> query = _context.VocablLists
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
        listInfoDtos = await _context.VocablLists
                                      .AsNoTracking()
                                      .Where(vl => vl.DeletedDate == null)
                                      .ProjectToInfoDto()
                                      .ToArrayAsync();
        return listInfoDtos;
    }

    public async Task<VocabListDto> Add(VocabListDto dto)
    {
        DateTime transactionTimeStamp = DateTime.UtcNow;
        VocabList entity;

        // TODO: Refactor to add whole graph at once.
        entity = dto.ToEntityWithoutListItems(transactionTimeStamp);
        _context.Add(entity);

        if (dto.ListItems.Any())
        {
            VocabListItem[] listItems;
            listItems = dto.ListItems.ToArray()
                           .ToEntities(transactionTimeStamp, entity.Id);
            _context.AddRange(listItems);
        }

        await _context.SaveChangesAsync();

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

        VocabList existingList = await _context.VocablLists
                                               .TryGetFirstActiveWithId(listId);

        dto.CopyListDetails(existingList, currentTimestamp);

        //TODO: Refactor the below to a separate repository with unit of work pattern.
        IEnumerable<VocabListItem> existingListItems = existingList.ListItems;
        bool allItemsDeleted = CheckDeleteAllListItems(existingListItems,
                                                       dto.ListItems,
                                                       currentTimestamp);
        if (allItemsDeleted)
        {
            await _context.SaveChangesAsync();
            return;
        }

        SoftDeletedRemovedListItems(dto, currentTimestamp, existingListItems);

        Dictionary<Guid, VocabListItem> nonDeletedListItemEntities;
        nonDeletedListItemEntities = existingListItems.Where(li => li.DeletedDate == null)
                                                      .ToDictionary(li => li.Id);
        AddOrUpdateListItems(dto, currentTimestamp, nonDeletedListItemEntities);

        await _context.SaveChangesAsync();
        return;
    }

    public async Task<bool> HardDelete(Guid id)
    {
        VocabList list;
        try
        {
            list = await _context.VocablLists
                                 .Include(vl => vl.ListItems)
                                 .FirstAsync(vl => vl.Id == id);
        }
        catch (InvalidOperationException)
        {
            return false;
        }

        if (list.ListItems.Any())
        {
            list.ListItems.ForEach((item) => _context.VocablListItems.Remove(item));
        }
        _context.VocablLists.Remove(list);

        await _context.SaveChangesAsync();
        return true;
    }

    private void AddOrUpdateListItems(VocabListDto dto, DateTime currentTimestamp, Dictionary<Guid, VocabListItem> nonDeletedListItemEntities)
    {
        dto.ListItems.ForEach(item =>
        {
            VocabListItem newListItem = CheckAddNewListItem(item, currentTimestamp);
            if (newListItem != null)
            {
                _context.Add(newListItem);
                return;
            }

            TryUpdateListItem(item, nonDeletedListItemEntities, currentTimestamp);
        });
    }

    private static bool CheckDeleteAllListItems(IEnumerable<VocabListItem> existingListItems,
        IEnumerable<VocabListItemDto> updatedListItems, DateTime transactionTimestamp)
    {
        bool areAllListItemsDeleted = !updatedListItems.Any() && existingListItems.Any();
        if (areAllListItemsDeleted)
        {
            existingListItems.SoftDeleteAll(transactionTimestamp);
            return true;
        }
        return false;
    }

    private static void SoftDeletedRemovedListItems(VocabListDto updateDto, DateTime currentTimestamp, IEnumerable<VocabListItem> existingListItems)
    {
        Dictionary<Guid, VocabListItemDto> updatedListItems;
        updatedListItems = updateDto.ListItems
                                    .Where(li => li.Id.HasValue)
                                    .ToDictionary(li => li.Id.Value);

        existingListItems.SoftDeleteWhere(item => !updatedListItems.ContainsKey(item.Id), currentTimestamp);
    }

    private static VocabListItem? CheckAddNewListItem(VocabListItemDto updatedItemDto, DateTime transactionTimestamp)
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
        return newListItem;
    }

    private static void TryUpdateListItem(VocabListItemDto updatedItem, Dictionary<Guid, VocabListItem> entities,
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
        updatedItem.CopyTo(existingListItem, transactionTimestamp);
    }
}
