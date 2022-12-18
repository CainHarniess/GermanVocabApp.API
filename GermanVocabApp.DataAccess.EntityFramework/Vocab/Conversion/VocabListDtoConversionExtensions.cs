﻿using GermanVocabApp.DataAccess.EntityFramework.Vocab.Models;
using GermanVocabApp.DataAccess.Shared.Vocab.Models;

namespace GermanVocabApp.DataAccess.EntityFramework.Vocab.Conversion;

internal static class VocabListDtoConversionExtensions
{
    public static VocabList ToEntityWithListItems(this VocabListDto dto)
    {
        VocabList entity = dto.ToEntityWithoutListItems();

        if (!dto.ListItems.Any())
        {
            return entity;
        }
        entity.ListItems = dto.ListItems.Select(li => li.ToEntity()).ToArray();
        return entity;
    }

    public static VocabList ToEntityWithoutListItems(this VocabListDto dto)
    {
        var entity = new VocabList()
        {
            Name = dto.Name,
            Description = dto.Description,
            UpdatedDate = null,
            DeletedDate = null,
        };

        if (dto.Id.HasValue)
        {
            entity.Id = dto.Id.Value;
        }
        return entity;
    }

    public static void CopyListDetails(this VocabListDto updateDto,
                                       VocabList entity, DateTime updateTimestamp)
    {
        entity.Name = updateDto.Name;
        entity.Description = updateDto.Description;
        entity.UpdatedDate = updateTimestamp;
    }
}