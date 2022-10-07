namespace GermanVocabApp.Core.SourceGeneration.Builders.Inspection;

public interface IBuiltInTypeDictProvider
{
    Dictionary<string, string> Provide();
}