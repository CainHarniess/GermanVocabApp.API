﻿using FluentValidation;
using GermanVocabApp.Api.VocabLists.Contracts;
using GermanVocabApp.Shared.Validation;
using Osiris.FluentValidation;

namespace GermanVocabApp.Api.VocabLists.Validation.VocabLists;

public abstract class AbstractListRequestValidator<TListRequest> : AbstractValidator<TListRequest>
    where TListRequest : IVocabListRequest
{
    public AbstractListRequestValidator()
    {
        RuleFor(c => c.Name).NotNull();
        RuleFor(c => c.Name).StringLengthRange(ListValidationData.NameMinLength, ListValidationData.NameMaxLength);
        RuleFor(c => c.Description).StringLengthRange(ListValidationData.DescriptionMinLength, ListValidationData.DescriptionMaxLength);
    }
}