using GermanVocabApp.Core.SourceGeneration.Builders;
using GermanVocabApp.Core.SourceGeneration.Builders.Generators;
using System.Text;

namespace GermanVocabApp.Core.Tests.Unit.Generators;

public class ModelBuilderBuildMethodGeneratorTests
{
    private readonly StringBuilder _sb;
    private readonly ModelBuilderBuildMethodGenerator _generator;

    public ModelBuilderBuildMethodGeneratorTests()
    {
        _sb = new StringBuilder();
        _generator = new ModelBuilderBuildMethodGenerator(_sb);
    }

    [Fact]
    public void Append_ShouldAppendCorrectMethodDeclaration()
    {
        ModelBuilderPropertyInfo[] props = ModelBuilderTestDataUtilities.BuildPropertyInfoArray();
        string modelTypeName = "MyClass";

        _generator.Append(props, modelTypeName);
        string result = _sb.ToString();

        Assert.Equal($@"    public {modelTypeName} Build()
    {{
        return new {modelTypeName}
        {{
            PropertyOneName = _memberOneName,
            PropertyTwoName = _memberTwoName,
            PropertyThreeName = _memberThreeName,
        }};
    }}
", result);
    }
}
