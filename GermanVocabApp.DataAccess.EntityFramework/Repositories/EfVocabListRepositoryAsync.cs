using GermanVocabApp.Domain.Abstractions;
using GermanVocabApp.Domain.VocabListAggregate;
using Microsoft.EntityFrameworkCore;
using VocabListDomain = GermanVocabApp.Domain.VocabListAggregate.VocabList;
using VocabListEntity = GermanVocabApp.DataAccess.EntityFramework.Models.VocabList;

namespace GermanVocabApp.DataAccess.EntityFramework.Repositories;
public class EfVocabListRepositoryAsync : IVocabListRepositoryAsync
{
    private readonly VocabListDbContext _context;

    public EfVocabListRepositoryAsync(VocabListDbContext context)
    {
        _context = context;
    }

    public async Task Add(VocabListDomain list)
    {
        VocabListEntity listEntity = list.ToEntity();
        listEntity.CreatedDate = DateTime.UtcNow;
        listEntity.UpdatedDate = null;
        listEntity.DeletedDate = null;

        await _context.VocablLists.AddAsync(listEntity);
        await _context.SaveChangesAsync();
    }

    public async Task AddToList(Guid listId, VocabListItem item)
    {
        throw new NotImplementedException();
    }

    public async Task Edit(VocabList list)
    {
        VocabListEntity? entity = await _context.VocablLists.Where(vl => vl.Id == list.Id)
                                                            .FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new KeyNotFoundException($"No object with id {list.Id}.");
        }

        list.CopyTo(entity);
        entity.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task EditListItem(Guid listId, VocabListItem updatedItem)
    {
        throw new NotImplementedException();
    }

    public async Task<VocabListDomain?> Get(Guid listId)
    {
        VocabListEntity? entity = await _context.VocablLists
                                                .FirstOrDefaultAsync(vl => vl.Id == listId);

        if (entity == null)
        {
            throw new KeyNotFoundException($"No object with id {listId}.");
        }
        return entity.ToDomainObject();
    }

    public async Task<IEnumerable<VocabListDomain>> GetAll()
    {
        return await _context.VocablLists.Where(vl => vl.DeletedDate == null)
                                         .Select(vl => vl.ToDomainObject())
                                         .ToArrayAsync();
                                    
    }

    public async Task<IEnumerable<VocabListItem>> GetListItems(Guid listId)
    {
        throw new NotImplementedException();
    }

    public async Task Remove(Guid listId)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveFromList(Guid listId, Guid itemId)
    {
        throw new NotImplementedException();
    }
}
