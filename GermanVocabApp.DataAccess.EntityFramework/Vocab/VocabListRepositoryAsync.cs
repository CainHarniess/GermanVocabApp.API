using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.EntityFramework.Core;
using GermanVocabApp.DataAccess.EntityFramework.Vocab.Conversion;
using GermanVocabApp.DataAccess.EntityFramework.Vocab.Models;
using GermanVocabApp.DataAccess.EntityFramework.Vocab.Projection;
using GermanVocabApp.DataAccess.EntityFramework.Vocab.QueryExtensions;
using GermanVocabApp.DataAccess.Shared.Vocab;
using GermanVocabApp.DataAccess.Shared.Vocab.Models;
using Microsoft.EntityFrameworkCore;
using Osiris.Utilities.Collections.Generic;

namespace GermanVocabApp.DataAccess.EntityFramework.Vocab;

public class VocabListRepositoryAsync : RepositoryBase, IVocabListRepositoryAsync
{
    public VocabListRepositoryAsync(GermanAppAppDbContext context) : base(context)
    {

    }

    public async Task<VocabListDto?> Get(Guid id)
    {
        IQueryable<VocabList> query = Context.Lists
                                              .AsNoTracking()
                                              .Where(vl => vl.Id == id
                                                        && vl.DeletedDate == null)
                                              .ProjectToListWithItems(id);
        VocabList entity = await query.SingleOrDefaultAsync();

        return entity?.ToDto();
    }

    public async Task<IEnumerable<VocabListInfoDto>> GetVocabListInfos()
    {
        IEnumerable<VocabListInfoDto> listInfoDtos;
        listInfoDtos = await Context.Lists
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

        VocabList existingList = await Context.Lists
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

        SoftDeletedRemovedListItems(dto, existingListItems);

        Dictionary<Guid, VocabListItem> nonDeletedListItemEntities;
        nonDeletedListItemEntities = existingListItems.Where(i => i.DeletedDate.HasValue == false)
                                                      .ToDictionary(i => i.Id);
        AddOrUpdateListItems(dto, nonDeletedListItemEntities);

        await Context.SaveChangesAsync();
    }

    public async Task<bool> HardDelete(Guid id)
    {
        VocabList? list = await Context.Lists
                                       .Include(vl => vl.ListItems)
                                       .FirstOrDefaultAsync(vl => vl.Id == id
                                                               && vl.DeletedDate == null);
        if (list == null)
        {
            return false;
        }

        if (list.ListItems.Any())
        {
            HardDeleteRangeWhere(list.ListItems, li => true);
        }
        Context.Lists.Remove(list);

        await Context.SaveChangesAsync();
        return true;
    }

    private void AddOrUpdateListItems(VocabListDto dto, Dictionary<Guid, VocabListItem> nonDeletedListItemEntities)
    {
        dto.ListItems.ForEach(item =>
        {
            VocabListItem newListItem = TryCreateItemNewItem(item);
            if (newListItem != null)
            {
                Context.Add(newListItem);
                return;
            }
            TryUpdateListItem(item, nonDeletedListItemEntities);
        });
    }

    private bool CheckDeleteAllListItems(IEnumerable<VocabListItem> existingListItems,
        IEnumerable<VocabListItemDto> updatedListItems)
    {
        bool areAllListItemsDeleted = !updatedListItems.Any() && existingListItems.Any();
        if (areAllListItemsDeleted)
        {
            SoftDeleteRangeWhere(existingListItems, l => true);
            return true;
        }
        return false;
    }

    private void SoftDeletedRemovedListItems(VocabListDto updateDto,
                                             IEnumerable<VocabListItem> existingListItems)
    {
        Dictionary<Guid, VocabListItemDto> updatedListItems;
        updatedListItems = updateDto.ListItems
                                    .Where(li => li.Id.HasValue)
                                    .ToDictionary(li => li.Id.Value);
        SoftDeleteRangeWhere(existingListItems, item => !updatedListItems.ContainsKey(item.Id));
    }

    private static VocabListItem? TryCreateItemNewItem(VocabListItemDto updatedItemDto)
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

    private static void TryUpdateListItem(VocabListItemDto updatedItem,
                                   Dictionary<Guid, VocabListItem> entities)
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
