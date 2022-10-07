using GermanVocabApp.Core.SourceGeneration.Builders;
using GermanVocabApp.Core.SourceGeneration.Builders.Generators;
using System.Text;

namespace GermanVocabApp.Core.Tests.Unit.Generators;

public class ModelBuilderClassGeneratorTests
{
    private readonly StringBuilder _sb;
    private readonly ModelBuilderMemberGenerator _memberGenerator;
    private readonly ModelBuilderWithMethodGenerator _withMethodGenerator;
    private readonly ModelBuilderBuildMethodGenerator _buildMethodGenerator;
    private readonly ModelBuilderClassGenerator _generator;

    public ModelBuilderClassGeneratorTests()
    {
        _sb = new StringBuilder();
        _memberGenerator = new ModelBuilderMemberGenerator(_sb);
        _withMethodGenerator = new ModelBuilderWithMethodGenerator(_sb);
        _buildMethodGenerator = new ModelBuilderBuildMethodGenerator(_sb);
        _generator = new ModelBuilderClassGenerator(_sb, _memberGenerator, _withMethodGenerator,
                                                         _buildMethodGenerator);
    }

    [Fact]
    public void Append_ShouldGenerateClassDeclaration()
    {
        ModelBuilderPropertyInfo[] props = ModelBuilderTestDataUtilities.BuildPropertyInfoArray();
        var builderInfo = new ModelBuilderInfo("MyModelBuilder", "MyModel", props);


        _generator.Append(builderInfo);
        string result = _sb.ToString();
        string expected = $@"[GeneratedCode]
public partial class {builderInfo.TypeName} : AbstractBuilder<{builderInfo.ModelTypeName}>
{{
    private {props[0].MemberTypeName} {props[0].MemberName};
    public {builderInfo.TypeName} With{props[0].PropertyName}({props[0].MemberTypeName} value)
    {{
        {props[0].MemberName} = value;
        return this;
    }}

    private {props[1].MemberTypeName} {props[1].MemberName};
    public {builderInfo.TypeName} With{props[1].PropertyName}({props[1].MemberTypeName} value)
    {{
        {props[1].MemberName} = value;
        return this;
    }}

    private {props[2].MemberTypeName} {props[2].MemberName};
    public {builderInfo.TypeName} With{props[2].PropertyName}({props[2].MemberTypeName} value)
    {{
        {props[2].MemberName} = value;
        return this;
    }}

    public {builderInfo.ModelTypeName} Build()
    {{
        return new {builderInfo.ModelTypeName}
        {{
            {props[0].PropertyName} = {props[0].MemberName},
            {props[1].PropertyName} = {props[1].MemberName},
            {props[2].PropertyName} = {props[2].MemberName},
        }};
    }}
}}
";

        Assert.Equal(expected, result);
    }
}


