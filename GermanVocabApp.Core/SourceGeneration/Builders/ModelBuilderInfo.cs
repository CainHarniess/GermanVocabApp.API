namespace GermanVocabApp.Core.SourceGeneration.Builders;

public class ModelBuilderInfo
{
    public ModelBuilderInfo(string typeName, string modelTypeName, ModelBuilderPropertyInfo[] properties)
    {
        TypeName = typeName;
        ModelTypeName = modelTypeName;
        Properties = properties;
    }

    public string TypeName { get; }
    public string ModelTypeName { get; }
    public ModelBuilderPropertyInfo[] Properties { get; }
}
