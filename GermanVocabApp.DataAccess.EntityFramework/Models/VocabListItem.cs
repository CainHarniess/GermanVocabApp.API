﻿
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.DataAccess.EntityFramework.Models;

public class VocabListItem : EntityBase
{
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
    public string German { get; set; } // TODO remove default
    public string? Plural { get; set; }
    public string? Preposition { get; set; }
    public Case? PrepositionCase { get; set; }
    public string? Comparative { get; set; }
    public string? Superlative { get; set; }
    public string English { get; set; } // TODO remove default
    public Guid VocabListId { get; set; }
    public virtual VocabList VocabList { get; set; }
    public FixedPlurality? FixedPlurality { get; set; }
}