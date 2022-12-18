using Osiris.Utilities.Collections.Generic;

namespace GermanVocabApp.DataAccess.EntityFramework.Core;

public abstract class RepositoryBase
{
    private readonly GermanAppAppDbContext _context;

    public RepositoryBase(GermanAppAppDbContext context)
    {
        _context = context;
    }

    protected GermanAppAppDbContext Context => _context;

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
