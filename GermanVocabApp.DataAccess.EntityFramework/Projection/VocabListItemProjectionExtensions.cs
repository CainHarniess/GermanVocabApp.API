using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.DataAccess.EntityFramework.Projection;

public static class VocabListItemProjectionExtensions
{
    public static IQueryable<VocabListInfoDto> ProjectToInfoDto(this IQueryable<VocabList> query)
    {
        IQueryable<VocabListInfoDto> infoQuery;
        infoQuery = query.Select(vl => new VocabListInfoDto()
        {
            Id = vl.Id,
            Name = vl.Name,
            Description = vl.Description,
        });
        return infoQuery;
    }
}
