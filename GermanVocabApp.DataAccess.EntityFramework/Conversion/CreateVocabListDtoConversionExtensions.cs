using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using GermanVocabApp.Shared.Data;
using Osiris.Utilities.Collections.Generic;

namespace GermanVocabApp.DataAccess.EntityFramework.Conversion;

internal static class CreateVocabListDtoConversionExtensions
{
    public static VocabList ToEntityWithListItems(this CreateVocabListDto dto, DateTime creationTimeStamp)
    {
        VocabList entity = dto.ToEntityWithoutListItems(creationTimeStamp);

        if (!dto.ListItems.Any())
        {
            return entity;
        }
        entity.ListItems = dto.ListItems.Select(li => li.ToEntity(creationTimeStamp));
        return entity;
    }

    public static VocabList ToEntityWithoutListItems(this CreateVocabListDto dto, DateTime creationTimeStamp)
    {
        return new VocabList()
        {
            Name = dto.Name,
            Description = dto.Description,
            CreatedDate = creationTimeStamp,
            UpdatedDate = null,
            DeletedDate = null,
        };
    }
}