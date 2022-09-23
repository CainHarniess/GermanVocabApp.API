using FluentValidation;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Shared.Validation;

namespace GermanVocabApp.Api.VocabLists.Validation.VocabLists;

public class CreateVocabListRequestValidator : AbstractValidator<CreateVocabListRequest>
{
    public CreateVocabListRequestValidator()
    {
        RuleFor(c => c.Name).MinimumLength(VocabListValidationData.NameMinLength)
                            .MaximumLength(VocabListValidationData.NameMaxLength);

        RuleFor(c => c.Description).MinimumLength(VocabListValidationData.DescriptionMinLength)
                                   .MaximumLength(VocabListValidationData.DescriptionMaxLength);
    }
}
