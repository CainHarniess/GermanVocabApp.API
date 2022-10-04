using GermanVocabApp.DataAccess.EntityFramework.Models;

namespace GermanVocabApp.DataAccess.EntityFramework.Tests.Unit.Builders;

public abstract class EntityBuilder<TEntity> : BuilderBase<TEntity> where TEntity : EntityBase
{
    protected Guid _id;
    protected DateTime? _createdDate;
    protected DateTime? _updatedDate;
    protected DateTime? _deletedDate;

    protected void ApplyBaseValues(TEntity result)
    {
        result.Id = _id;
        result.CreatedDate = _createdDate ?? DateTime.UtcNow;
        result.UpdatedDate = _updatedDate;
        result.DeletedDate = _deletedDate;
    }

    public EntityBuilder<TEntity> WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public EntityBuilder<TEntity> WithCreatedDate(DateTime date)
    {
        _createdDate = date;
        return this;
    }

    public EntityBuilder<TEntity> WithUpdatedDate(DateTime date)
    {
        _updatedDate = date;
        return this;
    }

    public EntityBuilder<TEntity> WithDeletedDate(DateTime date)
    {
        _deletedDate = date;
        return this;
    }
}
