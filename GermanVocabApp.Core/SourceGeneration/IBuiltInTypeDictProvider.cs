namespace GermanVocabApp.Core.SourceGeneration;

public interface IBuiltInTypeDictProvider
{
    Dictionary<string, string> Provide();
}