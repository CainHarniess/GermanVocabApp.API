namespace GermanVocabApp.Core.SourceGeneration.Builders;

public class ModelBuilderPropertyInfo
{
    public ModelBuilderPropertyInfo(string memberName, string memberTypeName, string propertyName)
    {
        MemberName = memberName;
        MemberTypeName = memberTypeName;
        PropertyName = propertyName;
    }

    public string MemberName { get; }
    public string MemberTypeName { get; }
    public string PropertyName { get; }
}
