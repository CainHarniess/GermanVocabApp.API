﻿using GermanVocabApp.Domain.Core;
using GermanVocabApp.Domain.Data;

namespace GermanVocabApp.Domain.VocabListAggregate;

public class VocabListItem : Entity
{
    public WordType WordType { get; set; }
    public bool? IsWeakMasculineNoun { get; set; }
    public Case? ReflexiveCase { get; set; }
    public bool? IsSeparable { get; set; }
    public bool? IsTransitive { get; set; }
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
    public Guid VocabListId { get; set; }
}