using GermanVocabApp.DataAccess.EntityFramework.Vocab.Models;

namespace GermanVocabApp.DataAccess.EntityFramework.Vocab.Projection;

public static class VocabListItemProjectionExtensions
{
    public static IEnumerable<VocabListItem> ProjectToFullItem(this IEnumerable<VocabListItem> query)
    {
        return query.Select(li => new VocabListItem()
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
        });
    }
}
