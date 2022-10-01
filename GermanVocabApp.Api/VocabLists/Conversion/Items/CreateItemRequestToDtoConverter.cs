using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.Api.VocabLists.Conversion.Items;

public class CreateItemRequestToDtoConverter : IConverter<ItemRequest, VocabListItemDto>
{
    public VocabListItemDto Convert(ItemRequest source)
    {
        return new VocabListItemDto()
        {
            Id = null,
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
            VocabListId = null,
            FixedPlurality = source.FixedPlurality,
        };
    }
}
