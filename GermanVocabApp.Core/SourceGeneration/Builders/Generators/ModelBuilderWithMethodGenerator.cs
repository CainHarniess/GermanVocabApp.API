using System.Text;

namespace GermanVocabApp.Core.SourceGeneration.Builders.Generators;

public class ModelBuilderWithMethodGenerator : AbstractSourceGenerator
{
    public ModelBuilderWithMethodGenerator(StringBuilder sb) : base(sb)
    {

    }

    public void Append(ModelBuilderPropertyInfo propertyInfo, string builderTypeName)
    {
        Sb.AppendLine($@"    public {builderTypeName} With{propertyInfo.PropertyName}({propertyInfo.MemberTypeName} value)
    {{
        {propertyInfo.MemberName} = value;
        return this;
    }}
");
    }
}