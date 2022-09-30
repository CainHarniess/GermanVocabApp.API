﻿using GermanVocabApp.Core.Contracts;

namespace GermanVocabApp.Api.VocabLists.Models;

public class CreateVocabListRequest : IListRequest<CreateVocabListItemRequest>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public IEnumerable<CreateVocabListItemRequest> ListItems { get; set; }
}