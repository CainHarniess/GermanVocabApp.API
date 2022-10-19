using GermanVocabApp.Core;
using GermanVocabApp.DataAccess.Shared.DataTransfer;

namespace GermanVocabApp.DataAccess.Models.Builders;

public abstract class EntityDtoBuilder<TEntityDto, TBuilder> : AbstractBuilder<TEntityDto>
    where TBuilder : EntityDtoBuilder<TEntityDto, TBuilder>
    where TEntityDto : EntityDtoBase
{
    protected Guid? _id;

    abstract protected TBuilder Instance { get; }

    public TBuilder WithId(Guid? id)
    {
        _id = id;
        return Instance;
    }

    public virtual TBuilder AsNew()
    {
        return WithId(null);
    }

    protected override void ApplyValues(TEntityDto result)
    {
        result.Id = _id;
    }

    protected override void Clear()
    {
        _id = default;
    }
}
