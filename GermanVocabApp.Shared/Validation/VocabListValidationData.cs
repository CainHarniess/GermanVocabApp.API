namespace GermanVocabApp.Shared.Validation;
public static class VocabListValidationData
{
    public const int NameMinLength = 3;
    public const int NameMaxLength = 100;

    public const int DescriptionMinLength = 3;
    public const int DescriptionMaxLength = 250;
}

public static class VocabListItemValidationData
{
    public const int VerbMinLength = 3;
    public const int VerbMaxLength = 25;

    public const int GermanMinLength = 3;
    public const int GermanMaxLength = 100;

    public const int EnglishMinLength = 3;
    public const int EnglishMaxLength = 100;
}
