﻿using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Validation.DependencyInjection;

namespace GermanVocabApp.Api.VocabLists.Validation.VocabLists;

public class CreateListValidator : AbstractListValidator<CreateVocabListRequest, CreateVocabListItemRequest>
{
    public CreateListValidator(IValidator<CreateVocabListItemRequest> itemValidator)
        : base(itemValidator)
    {

    }
}
