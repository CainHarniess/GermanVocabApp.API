using FluentValidation;
using GermanVocabApp.Api.VocabLists.Models;

namespace GermanVocabApp.Api.VocabLists.Validation;

public class CreateVocabListRequestValidator : AbstractValidator<CreateVocabListRequest>
{
    public CreateVocabListRequestValidator()
    {
        RuleFor(c => c.Name).MinimumLength(3).MaximumLength(100);
        RuleFor(c => c.Description).MinimumLength(3).MaximumLength(250);
    }
}
