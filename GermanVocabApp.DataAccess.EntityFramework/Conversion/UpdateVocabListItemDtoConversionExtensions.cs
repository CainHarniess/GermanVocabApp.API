using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.DataAccess.EntityFramework.Conversion;

internal static class UpdateVocabListItemDtoConversionExtensions
{
    public static VocabListItem ToNewEntity(this UpdateVocabListItemDto updateDto, DateTime updateTimestamp)
    {
        if (updateDto.Id.HasValue)
        {
            throw new UnexpectedIdException($"Vocab list item with ID {updateDto.Id.Value} provided when a null ID was expected.");
        }
        VocabListItem entity = new VocabListItem();
        updateDto.CopyTo(entity, updateTimestamp);
        return entity;
    }

    public static void CopyTo(this UpdateVocabListItemDto updateDto,
                              VocabListItem entity, DateTime updateTimestamp)
    {
        entity.WordType = updateDto.WordType;
        entity.IsWeakMasculineNoun = updateDto.IsWeakMasculineNoun;
        entity.ReflexiveCase = updateDto.ReflexiveCase;
        entity.Separability = updateDto.Separability;
        entity.Transitivity = updateDto.Transitivity;
        entity.ThirdPersonPresent = updateDto.ThirdPersonPresent;
        entity.ThirdPersonImperfect = updateDto.ThirdPersonImperfect;
        entity.AuxiliaryVerb = updateDto.AuxiliaryVerb;
        entity.Perfect = updateDto.Perfect;
        entity.Gender = updateDto.Gender;
        entity.German = updateDto.German;
        entity.Plural = updateDto.Plural;
        entity.Preposition = updateDto.Preposition;
        entity.PrepositionCase = updateDto.PrepositionCase;
        entity.Comparative = updateDto.Comparative;
        entity.Superlative = updateDto.Superlative;
        entity.English = updateDto.English;
        entity.VocabListId = updateDto.VocabListId;
        entity.FixedPlurality = updateDto.FixedPlurality;
        entity.UpdatedDate = updateTimestamp;
    }
}
