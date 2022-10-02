﻿using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.DataAccess.EntityFramework.Projection;

public static class VocabListProjectionExtensions
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

    public static IQueryable<VocabList> ProjectToListWithItems(this IQueryable<VocabList> query, Guid id)
    {
        return query.Select(vl => new VocabList()
        {
            Id = vl.Id,
            Name = vl.Name,
            Description = vl.Description,
            ListItems = vl.ListItems
                          .Where(li => li.VocabListId == id
                                    && li.DeletedDate == null)
                          .ProjectToFullItem()
        });
    }
}
