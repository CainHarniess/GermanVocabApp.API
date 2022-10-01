using GermanVocabApp.Core.Contracts;

namespace GermanVocabApp.Api.VocabLists.Conversion;

public class AggregateChildResourceConverter<TSource, TTarget> : IChildResourceConverter<TSource[], TTarget[]>
{
    private readonly IChildResourceConverter<TSource, TTarget> _itemConverter;

    public AggregateChildResourceConverter(IChildResourceConverter<TSource, TTarget> itemConverter)
    {
        _itemConverter = itemConverter;
    }

    public TTarget[] Convert(TSource[] source, Guid parentId)
    {
        TTarget[] responseArray = new TTarget[source.Length];
        for (int i = 0; i < source.Length; i++)
        {
            var dto = source[i];
            var response = _itemConverter.Convert(dto, parentId);
            responseArray[i] = response;
        }
        return responseArray;
    }
}
