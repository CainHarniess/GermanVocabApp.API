using FluentValidation;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Shared.Validation;

namespace GermanVocabApp.Api.VocabLists.Validation.VocabLists;

public class UpdateVocabListRequestValidator : AbstractValidator<UpdateVocabListRequest>
{
    public UpdateVocabListRequestValidator()
    {
        RuleFor(c => c.Name).MinimumLength(VocabListValidationData.NameMinLength)
                            .MaximumLength(VocabListValidationData.NameMaxLength);

        RuleFor(c => c.Description).MinimumLength(VocabListValidationData.DescriptionMinLength)
                                   .MaximumLength(VocabListValidationData.DescriptionMaxLength);
    }
}
