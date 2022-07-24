using VocabListDomain = GermanVocabApp.Domain.VocabListAggregate.VocabList;
using VocabListEntity = GermanVocabApp.DataAccess.EntityFramework.Models.VocabList;

namespace GermanVocabApp.DataAccess.EntityFramework;

internal static class VocabListEntityExtensions
{
    public static VocabListDomain ToDomainObject(this VocabListEntity entity)
    {
        return new VocabListDomain()
        { 
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description
        };
    }
}
