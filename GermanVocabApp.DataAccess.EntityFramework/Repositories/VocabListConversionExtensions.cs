using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.DataAccess.EntityFramework.Repositories;

public static class VocabListConversionExtensions
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
        VocabListDto dto = new VocabListDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
        };

        if (entity.ListItems == null)
        {
            throw new NullReferenceException("VocabList entity ListItems property returned null." +
                                             "\n\nIs a null value acceptable?");
        }

        dto.ListItems = entity.ListItems.ToDtos();
        return dto;
    }
}

public static class VocabListItemConversionExtensions
{
    public static IEnumerable<VocabListItemDto> ToDtos(this IEnumerable<VocabListItem> entities)
    {
        return entities.Select(vli => vli.ToDto());
    }

    public static VocabListItemDto ToDto(this VocabListItem entity)
    {
        return new VocabListItemDto()
        {
            Id = entity.Id,
            WordType = entity.WordType,
            ReflexiveCase = entity.ReflexiveCase,
            IsSeparable = entity.IsSeparable,
            IsTransitive = entity.IsTransitive,
            ThirdPersonPresent = entity.ThirdPersonPresent,
            ThirdPersonImperfect = entity.ThirdPersonImperfect,
            AuxiliaryVerb = entity.AuxiliaryVerb,
            Perfect = entity.Perfect,
            Gender = entity.Gender,
            German = entity.German,
            Plural = entity.Plural,
            Preposition = entity.Preposition,
            PrepositionCase = entity.PrepositionCase,
            Comparative = entity.Comparative,
            Superlative = entity.Superlative,
            English = entity.English,
            VocabListId = entity.VocabListId,
        };
    }
}
