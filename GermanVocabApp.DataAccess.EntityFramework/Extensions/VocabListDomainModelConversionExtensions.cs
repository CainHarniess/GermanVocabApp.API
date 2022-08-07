using VocabListDomain = GermanVocabApp.Domain.VocabListAggregate.VocabList;
using VocabListEntity = GermanVocabApp.DataAccess.EntityFramework.Models.VocabList;

namespace GermanVocabApp.DataAccess.EntityFramework;
internal static class VocabListDomainModelConversionExtensions
{
    public static VocabListEntity ToEntity(this VocabListDomain domainModel)
    {
        VocabListEntity entity = new VocabListEntity();
        domainModel.CopyTo(entity);
        return entity;
    }

    public static void CopyTo(this VocabListDomain domainModel, VocabListEntity dataEntity)
    {
        dataEntity.Name = domainModel.Name;
        dataEntity.Description = domainModel.Description;
        dataEntity.ListItems = null;
    }
}