using GermanVocabApp.Core.SourceGeneration.Builders;
using GermanVocabApp.Core.SourceGeneration.Builders.Generators;
using System.Text;

namespace GermanVocabApp.Core.Tests.Unit.Generators;

public class ModelBuilderWithMethodGeneratorTests
{
    private readonly StringBuilder _sb;
    private readonly ModelBuilderWithMethodGenerator _generator;

    public ModelBuilderWithMethodGeneratorTests()
    {
        _sb = new StringBuilder();
        _generator = new ModelBuilderWithMethodGenerator(_sb);
    }

    [Fact]
    public void Append_ShouldGenerateCorrectMemberDeclaration()
    {
        var propInfo = new ModelBuilderPropertyInfo("_memberName", "MemberType", "PropertyName");
        string builderTypeName = "MyClassBuilder";

        _generator.Append(propInfo, builderTypeName);
        string result = _sb.ToString();

        Assert.Equal($@"    public {builderTypeName} WithPropertyName(MemberType value)
    {{
        _memberName = value;
        return this;
    }}

", result);
    }
}