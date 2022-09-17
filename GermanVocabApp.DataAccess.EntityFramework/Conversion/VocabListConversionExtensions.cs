using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.DataAccess.EntityFramework.Conversion;

internal static class VocabListConversionExtensions
{
    public static VocabListInfoDto ToInfoDto(this VocabList entity)
    {
        return new VocabListInfoDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
        };
    }

    public static VocabListDto ToDto(this VocabList entity)
    {
        var dto = new VocabListDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
        };

        if (entity.ListItems == null)
        {
            dto.ListItems = Array.Empty<VocabListItemDto>();
        }
        else
        {
            dto.ListItems = entity.ListItems.ToDtos();
        }

        return dto;
    }
}