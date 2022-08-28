using GermanVocabApp.DataAccess.EntityFramework.Conversion;
using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.EntityFramework.Projection;
using GermanVocabApp.DataAccess.Shared;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Microsoft.EntityFrameworkCore;

namespace GermanVocabApp.DataAccess.EntityFramework.Repositories;

public class VocabListRepositoryAsync : IVocabListRepositoryAsync
{
    private readonly VocabListDbContext _context;

    public VocabListRepositoryAsync(VocabListDbContext context)
    {
        _context = context;
    }

    public async Task<VocabListDto?> Get(Guid listId)
    {
        IQueryable<VocabList> query = _context.VocablLists
                                              .Where(vl => vl.Id == listId
                                                           && vl.DeletedDate == null)
                                              .Include(vl => vl.ListItems
                                                               .Where(li => li.DeletedDate == null));

        query = query.Select(vl => new VocabList()
                      {
                      Id = vl.Id,
                      Name = vl.Name,
                      Description = vl.Description,
                      ListItems = vl.ListItems
                                    .Select(li => new VocabListItem()
                                    {
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
                                    }),
                      });

        VocabList entity = await query.SingleOrDefaultAsync();

        if (entity == null)
        {
            return null;
        }

        if (entity.ListItems == null)
        {
            throw new NullReferenceException($"VocabList entity ListItems property returned null in {nameof(VocabListRepositoryAsync)}.{nameof(Get)}." +
                                             $"\n\nCheck Entity Framework is behaving as expected.");
        }
        return entity.ToDto();
    }

    public async Task<IEnumerable<VocabListInfoDto>> GetVocabListInfos()
    {
        IEnumerable<VocabListInfoDto> listInfoDtos;
        listInfoDtos =  await _context.VocablLists
                                      .Where(vl => vl.DeletedDate == null)
                                      .ProjectToInfoDto()
                                      .ToArrayAsync();
        return listInfoDtos;
    }

    public async Task<Guid> Add(CreateVocabListDto dto)
    {
        DateTime transactionTimeStamp = DateTime.UtcNow;
        VocabList entity;

        entity = dto.ToEntityWithoutListItems(transactionTimeStamp);
        _context.Add(entity);
        
        if (dto.ListItems.Any())
        {
            IEnumerable<VocabListItem> listItems = dto.ListItems
                                                      .ToEntities(transactionTimeStamp, entity.Id);
            _context.AddRange(listItems);
        }

        await _context.SaveChangesAsync();
        return entity.Id;
    }
}
