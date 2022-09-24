namespace GermanVocabApp.Shared.Validation;
public struct ListValidationData
{
    public const int NameMinLength = 3;
    public const int NameMaxLength = 100;

    public const int DescriptionMinLength = 3;
    public const int DescriptionMaxLength = 250;
}

public struct ListItemValidationData
{
    public const int GermanMinLength = 3;
    public const int GermanMaxLength = 100;

    public const int EnglishMinLength = 3;
    public const int EnglishMaxLength = 100;

    public const int PrepositionMinLength = 2;
    public const int PrepositionMaxLength = 25;
}

public struct VerbValidationData
{
    public const int VerbMinLength = 3;
    public const int VerbMaxLength = 25;

    public const int GermanMinLength = 3;
    public const int GermanMaxLength = 100;

    public const int EnglishMinLength = 3;
    public const int EnglishMaxLength = 100;
}

public struct ModifierValidationData
{
    public const int ComparativeMinLength = 3;
    public const int ComparativeMaxLength = 100;

    public const int SuperlativeMinLength = 3;
    public const int SuperlativeMaxLength = 100;
}