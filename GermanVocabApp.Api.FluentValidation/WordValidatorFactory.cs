using GermanVocabApp.Api.FluentValidation.Validators;
using GermanVocabApp.Core.Contracts;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.Api.FluentValidation;

// TODO: Write unit tests.
public class WordValidatorFactory
{
    private Dictionary<WordType, FluentWordValidator> _validators;

    public WordValidatorFactory(FluentNounValidator nounValidator, FluentVerbValidator verbValidator,
                                FluentModifierValidator modifierValidator)
    {
        _validators = new Dictionary<WordType, FluentWordValidator>()
        {
            { WordType.Noun, nounValidator },
            { WordType.Verb, verbValidator },
            { WordType.Adjective, modifierValidator },
            { WordType.Adverb, modifierValidator },
        };
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
