using GermanVocabApp.Core.Contracts;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.Api.VocabLists.Models;

public class ItemRequest : IListItemRequest
{
    public Guid? Id { get; set; }
    public WordType WordType { get; set; }
    public bool? IsWeakMasculineNoun { get; set; }
    public ReflexiveCase? ReflexiveCase { get; set; }
    public Separability? Separability { get; set; }
    public Transitivity? Transitivity { get; set; }
    public string? ThirdPersonPresent { get; set; }
    public string? ThirdPersonImperfect { get; set; }
    public AuxiliaryVerb? AuxiliaryVerb { get; set; }
    public string? Perfect { get; set; }
    public Gender? Gender { get; set; }
    public string German { get; set; }
    public string? Plural { get; set; }
    public string? Preposition { get; set; }
    public Case? PrepositionCase { get; set; }
    public string? Comparative { get; set; }
    public string? Superlative { get; set; }
    public string English { get; set; }
    public FixedPlurality? FixedPlurality { get; set; }
}