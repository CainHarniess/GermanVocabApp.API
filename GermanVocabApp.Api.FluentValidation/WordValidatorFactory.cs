using GermanVocabApp.Api.FluentValidation.FluentValidators;
using GermanVocabApp.Api.VocabLists.Contracts;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.Api.FluentValidation;
internal class WordValidatorFactory
{
    private Dictionary<WordType, FluentWordValidator> _validators;

    public WordValidatorFactory()
    {
        _validators = new Dictionary<WordType, FluentWordValidator>(4);

        _validators.Add(WordType.Noun, new FluentNounValidator());
        _validators.Add(WordType.Verb, new FluentVerbValidator());

        var modiferValidator = new FluentModifierValidator();

        _validators.Add(WordType.Adjective, modiferValidator);
        _validators.Add(WordType.Adverb, modiferValidator);
    }

    public FluentWordValidator Create(IListItemRequest request)
    {
        WordType wordType = request.WordType;
        if (!_validators.ContainsKey(wordType))
        {
            throw new ArgumentException($"Invalid word type {Enum.GetName(wordType)} provided with value {wordType}.");
        }
        return _validators[wordType];
    }
}
