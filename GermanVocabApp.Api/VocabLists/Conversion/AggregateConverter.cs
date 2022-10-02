using GermanVocabApp.Core.Contracts;

namespace GermanVocabApp.Api.VocabLists.Conversion;

public class AggregateConverter<TSource, TTarget> : IConverter<TSource[], TTarget[]>
{
    private readonly IConverter<TSource, TTarget> _itemConverter;

    public AggregateConverter(IConverter<TSource, TTarget> itemConverter)
    {
        _itemConverter = itemConverter;
    }

    public TTarget[] Convert(TSource[] source)
    {
        TTarget[] responseArray = new TTarget[source.Length];
        for (int i = 0; i < source.Length; i++)
        {
            var dto = source[i];
            var response = _itemConverter.Convert(dto);
            responseArray[i] = response;
        }
        return responseArray;
    }
}