using GermanVocabApp.Core.SourceGeneration;
using System.Text;

namespace GermanVocabApp.Core.SourceGeneration.Builders.Generators;

public class ModelBuilderClassGenerator : AbstractSourceGenerator
{
    private readonly ModelBuilderMemberGenerator _memberGenerator;
    private readonly ModelBuilderWithMethodGenerator _withMethodGenerator;
    private readonly ModelBuilderBuildMethodGenerator _buildMethodGenerator;

    public ModelBuilderClassGenerator(StringBuilder sb, ModelBuilderMemberGenerator memberGenerator,
        ModelBuilderWithMethodGenerator withMethodGenerator,
        ModelBuilderBuildMethodGenerator buildMethodGenerator) : base(sb)
    {
        _memberGenerator = memberGenerator;
        _withMethodGenerator = withMethodGenerator;
        _buildMethodGenerator = buildMethodGenerator;
    }

    public void Append(ModelBuilderInfo builderInfo)
    {
        Sb.AppendLine($@"[GeneratedCode]
public partial class {builderInfo.TypeName} : AbstractBuilder<{builderInfo.ModelTypeName}>
{{");
        AppendClassBody(builderInfo);
        Sb.AppendLine($@"}}");
    }

    private void AppendClassBody(ModelBuilderInfo builderInfo)
    {
        var props = builderInfo.Properties;
        for (int i = 0; i < props.Length; i++)
        {
            var prop = props[i];
            _memberGenerator.Append(prop);
            _withMethodGenerator.Append(prop, builderInfo.TypeName);
        }
        _buildMethodGenerator.Append(props, builderInfo.ModelTypeName);
    }
}
