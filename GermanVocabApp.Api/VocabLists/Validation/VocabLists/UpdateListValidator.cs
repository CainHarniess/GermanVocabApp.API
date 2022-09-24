using GermanVocabApp.Api.VocabLists.Models;
using GermanVocabApp.Core.Validation.DependencyInjection;

namespace GermanVocabApp.Api.VocabLists.Validation.VocabLists;

public class UpdateListValidator : AbstractListValidator<UpdateVocabListRequest, UpdateVocabListItemRequest>
{
    public UpdateListValidator(IValidator<UpdateVocabListItemRequest> itemValidator)
        : base(itemValidator)
    {

    }
}
