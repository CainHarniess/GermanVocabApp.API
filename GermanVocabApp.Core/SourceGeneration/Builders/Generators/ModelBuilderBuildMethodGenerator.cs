using System.Text;

namespace GermanVocabApp.Core.SourceGeneration.Builders.Generators;

public class ModelBuilderBuildMethodGenerator : AbstractSourceGenerator
{
    public ModelBuilderBuildMethodGenerator(StringBuilder sb) : base(sb)
    {

    }

    public void Append(ModelBuilderPropertyInfo[] properties, string modelTypeName)
    {
        Sb.AppendLine($@"    public {modelTypeName} Build()
    {{
        return new {modelTypeName}
        {{");
        for (int i = 0; i < properties.Length; i++)
        {
            ModelBuilderPropertyInfo property = properties[i];
            Sb.AppendLine($@"            {property.PropertyName} = {property.MemberName},");
        }
        Sb.AppendLine($@"        }};");
        Sb.AppendLine($@"    }}");
    }
}
