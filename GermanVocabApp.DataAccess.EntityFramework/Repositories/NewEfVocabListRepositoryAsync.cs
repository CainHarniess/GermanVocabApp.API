using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.DataAccess.Shared;
using GermanVocabApp.DataAccess.Shared.DataTransfer;
using Microsoft.EntityFrameworkCore;

namespace GermanVocabApp.DataAccess.EntityFramework.Repositories;

public class NewEfVocabListRepositoryAsync : INewVocabListRepositoryAsync
{
    private readonly VocabListDbContext _context;

    public NewEfVocabListRepositoryAsync(VocabListDbContext context)
    {
        _context = context;
    }

    public async Task<VocabListDto?> Get(Guid listId)
    {
        VocabList entity = await _context.VocablLists
                                         .Where(vl => vl.Id == listId
                                                      && vl.DeletedDate == null)
                                         .Include(vl => vl.ListItems)
                                         .SingleOrDefaultAsync();

        if (entity == null)
        {
            return null;
        }

        if (entity.ListItems == null)
        {
            throw new NullReferenceException($"VocabList entity ListItems property returned null in {nameof(NewEfVocabListRepositoryAsync)}.{nameof(Get)}." +
                                             $"\n\nCheck Entity Framework is behaving as expected.");
        }
        return entity.ToDto();
    }

    public async Task<IEnumerable<VocabListInfoDto>> GetVocabListInfos()
    {
        IEnumerable<VocabListInfoDto> listInfoDtos;
        listInfoDtos =  await _context.VocablLists
                                      .Where(vl => vl.DeletedDate == null)
                                      .Select(vl => vl.ToInfoDto())
                                      .ToArrayAsync();
        return listInfoDtos;
    }
}
