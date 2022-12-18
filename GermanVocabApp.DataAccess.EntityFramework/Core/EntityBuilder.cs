using GermanVocabApp.Core;

namespace GermanVocabApp.DataAccess.EntityFramework.Core;

public abstract class EntityBuilder<TEntity, TBuilder> : AbstractBuilder<TEntity>
    where TBuilder : EntityBuilder<TEntity, TBuilder>
    where TEntity : EntityBase
{
    protected Guid _id;
    protected DateTime? _createdDate;
    protected DateTime? _updatedDate;
    protected DateTime? _deletedDate;

    protected abstract TBuilder Instance { get; }


    public TBuilder WithId(Guid id)
    {
        _id = id;
        return Instance;
    }

    public TBuilder WithCreatedDate(DateTime date)
    {
        _createdDate = date;
        return Instance;
    }

    public TBuilder WithUpdatedDate(DateTime date)
    {
        _updatedDate = date;
        return Instance;
    }

    public TBuilder WithDeletedDate(DateTime date)
    {
        _deletedDate = date;
        return Instance;
    }

    protected override void ApplyValues(TEntity result)
    {
        result.Id = _id;
        result.CreatedDate = _createdDate ?? DateTime.UtcNow;
        result.UpdatedDate = _updatedDate;
        result.DeletedDate = _deletedDate;
    }

    protected override void Clear()
    {
        _id = default;
        _createdDate = default;
        _updatedDate = default;
        _deletedDate = default;
    }
}
