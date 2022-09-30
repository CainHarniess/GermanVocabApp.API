﻿namespace GermanVocabApp.Core.Contracts;

public interface IListRequest<TItem>
    where TItem : IListItemRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public IEnumerable<TItem> ListItems { get; set; }
}