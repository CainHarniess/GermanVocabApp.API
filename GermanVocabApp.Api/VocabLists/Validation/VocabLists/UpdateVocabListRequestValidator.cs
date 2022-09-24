using FluentValidation;
using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Shared.Validation;
using Osiris.FluentValidation;

namespace GermanVocabApp.Api.VocabLists.Validation.VocabLists;

public class UpdateVocabListRequestValidator : AbstractValidator<UpdateVocabListRequest>
{
    public UpdateVocabListRequestValidator()
    {
        RuleFor(c => c.Name).StringLengthRange(ListValidationData.NameMinLength, ListValidationData.NameMaxLength);
        RuleFor(c => c.Description).StringLengthRange(ListValidationData.DescriptionMinLength, ListValidationData.DescriptionMaxLength);
    }
}
