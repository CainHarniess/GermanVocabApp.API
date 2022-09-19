﻿using GermanVocabApp.Core.Exceptions;
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
        // TODO: Refactor the below into a projection extension method.
        IQueryable<VocabList> query = _context.VocablLists
                                              .AsNoTracking()
                                              .Where(vl => vl.Id == id
                                                        && vl.DeletedDate == null)
                                              .Select(vl => new VocabList()
                                              {
                                                  Id = vl.Id,
                                                  Name = vl.Name,
                                                  Description = vl.Description,
                                                  ListItems = vl.ListItems
                                                                .Where(li => li.VocabListId == id
                                                                          && li.DeletedDate == null)
                                                                .Select(li => new VocabListItem()
                                                                {
                                                                    Id = li.Id,
                                                                    WordType = li.WordType,
                                                                    IsWeakMasculineNoun = li.IsWeakMasculineNoun,
                                                                    ReflexiveCase = li.ReflexiveCase,
                                                                    Separability = li.Separability,
                                                                    Transitivity = li.Transitivity,
                                                                    ThirdPersonPresent = li.ThirdPersonPresent,
                                                                    ThirdPersonImperfect = li.ThirdPersonImperfect,
                                                                    AuxiliaryVerb = li.AuxiliaryVerb,
                                                                    Perfect = li.Perfect,
                                                                    Gender = li.Gender,
                                                                    German = li.German,
                                                                    Plural = li.Plural,
                                                                    Preposition = li.Preposition,
                                                                    PrepositionCase = li.PrepositionCase,
                                                                    Comparative = li.Comparative,
                                                                    Superlative = li.Superlative,
                                                                    English = li.English,
                                                                    VocabListId = li.VocabListId,
                                                                    FixedPlurality = li.FixedPlurality,
                                                                })
                                              });
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

    public async Task<VocabListDto> Add(CreateVocabListDto creationDto)
    {
        DateTime transactionTimeStamp = DateTime.UtcNow;
        VocabList entity;

        // TODO: Refactor to add whole graph at once.
        entity = creationDto.ToEntityWithoutListItems(transactionTimeStamp);
        _context.Add(entity);

        if (creationDto.ListItems.Any())
        {
            IEnumerable<VocabListItem> listItems;
            listItems = creationDto.ListItems
                                   .ToEntities(transactionTimeStamp, entity.Id);
            _context.AddRange(listItems);
        }

        await _context.SaveChangesAsync();

        VocabListDto retrievalDto = entity.ToDto();
        return retrievalDto;
    }

    public async Task Update(UpdateVocabListDto updateDto)
    {
        DateTime currentTimestamp = DateTime.UtcNow;
        Guid listId = updateDto.Id;

        VocabList existingList = await _context.VocablLists
                                               .TryGetFirstActiveWithId(listId);

        updateDto.CopyListDetails(existingList, currentTimestamp);

        IEnumerable<VocabListItem> existingListItems = existingList.ListItems;
        bool areAllListItemsDeleted = !updateDto.ListItems.Any() && existingListItems.Any();
        if (areAllListItemsDeleted)
        {
            existingListItems.SoftDeleteAll(currentTimestamp);
            await _context.SaveChangesAsync();
            return;
        }

        Dictionary<Guid, UpdateVocabListItemDto> updatedListItems;
        updatedListItems = updateDto.ListItems
                                    .Where(li => li.Id.HasValue)
                                    .ToDictionary(li => li.Id.Value);
        existingListItems.SoftDeleteWhere(item => !updatedListItems.ContainsKey(item.Id), currentTimestamp);

        Dictionary<Guid, VocabListItem> nonDeletedListItemEntities;
        nonDeletedListItemEntities = existingListItems.Where(li => li.DeletedDate == null)
                                                      .ToDictionary(li => li.Id);
        updateDto.ListItems.ForEach(item =>
        {
            if (!item.Id.HasValue)
            {
                VocabListItem newListItem;
                try
                {
                    newListItem = item.ToNewEntity(currentTimestamp);
                }
                catch (UnexpectedIdException e)
                {
                    throw e;
                }
                _context.Add(newListItem);
                return;
            }

            Guid listItemId = item.Id.Value;
            if (!nonDeletedListItemEntities.ContainsKey(listItemId))
            {
                throw new EntityNotFoundException($"Vocab list item with ID {listItemId} not found in "
                                                + $"Vocab List with ID {listId}.");
            }
            VocabListItem existingListItem = nonDeletedListItemEntities[listItemId];
            item.CopyTo(existingListItem, currentTimestamp);
        });

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
}
