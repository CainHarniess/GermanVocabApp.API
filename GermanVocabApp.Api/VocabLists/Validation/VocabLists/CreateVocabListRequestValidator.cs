using FluentValidation;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Shared.Validation;
using Osiris.FluentValidation;

namespace GermanVocabApp.Api.VocabLists.Validation.VocabLists;

public class CreateVocabListRequestValidator : AbstractValidator<CreateVocabListRequest>
{
    public CreateVocabListRequestValidator()
    {
        RuleFor(c => c.Name).StringLengthRange(ListValidationData.NameMinLength, ListValidationData.NameMaxLength);
        RuleFor(c => c.Description).StringLengthRange(ListValidationData.DescriptionMinLength, ListValidationData.DescriptionMaxLength);
    }
}
