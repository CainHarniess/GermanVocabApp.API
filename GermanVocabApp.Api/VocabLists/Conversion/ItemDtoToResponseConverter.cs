using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.Core.Exceptions;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.VocabLists.Conversion;

public class ItemDtoToResponseConverter : IConverter<VocabListItemDto, ItemResponse>
{
    public ItemResponse Convert(VocabListItemDto source)
    {
        if (!source.Id.HasValue)
        {
            throw new UnexpectedNullIdException("Expect non-null list item ID when copying to response object.");
        }
        if (!source.VocabListId.HasValue)
        {
            throw new UnexpectedNullIdException("Expect list item to have non-null list ID when copying to response object.");
        }
        return new ItemResponse()
        {
            Id = source.Id.Value,
            WordType = source.WordType,
            IsWeakMasculineNoun = source.IsWeakMasculineNoun,
            ReflexiveCase = source.ReflexiveCase,
            Separability = source.Separability,
            Transitivity = source.Transitivity,
            ThirdPersonPresent = source.ThirdPersonPresent,
            ThirdPersonImperfect = source.ThirdPersonImperfect,
            AuxiliaryVerb = source.AuxiliaryVerb,
            Perfect = source.Perfect,
            Gender = source.Gender,
            German = source.German,
            Plural = source.Plural,
            Preposition = source.Preposition,
            PrepositionCase = source.PrepositionCase,
            Comparative = source.Comparative,
            Superlative = source.Superlative,
            English = source.English,
            VocabListId = source.VocabListId.Value,
            FixedPlurality = source.FixedPlurality,
        };
    }
}

