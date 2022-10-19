using GermanVocabApp.DataAccess.EntityFramework.Models;
using Osiris.Utilities.Collections.Generic;

namespace GermanVocabApp.DataAccess.EntityFramework.Repositories;

public abstract class RepositoryBase
{
    private readonly VocabListDbContext _context;

    public RepositoryBase(VocabListDbContext context)
    {
        _context = context;
    }

    protected VocabListDbContext Context => _context;

    public void SoftDeleteRangeWhere<TEntity>(IEnumerable<TEntity> entities, Predicate<TEntity> condition)
        where TEntity : EntityBase
    {
        entities.ForEach(entity =>
        {
            if (!condition(entity))
            {
                return;
            }
            entity.DeletedDate = DateTime.UtcNow;
            _context.Remove(entity);
        });
    }

    public void HardDeleteRangeWhere<TEntity>(IEnumerable<TEntity> entities, Predicate<TEntity> condition)
        where TEntity : EntityBase
    {
        entities.ForEach(entity =>
        {
            if (!condition(entity))
            {
                return;
            }
            _context.Remove(entity);
        });
    }
}
