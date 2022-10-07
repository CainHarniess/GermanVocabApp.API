using System.Text;

namespace GermanVocabApp.Core.SourceGeneration.Builders.Generators;
public class ModelBuilderMemberGenerator : AbstractSourceGenerator
{
    public ModelBuilderMemberGenerator(StringBuilder sb) : base(sb)
    {

    }

    public void Append(ModelBuilderPropertyInfo propertyInfo)
    {
        Sb.AppendLine($@"    private {propertyInfo.MemberTypeName} {propertyInfo.MemberName};");
    }
}
