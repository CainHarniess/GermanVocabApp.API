using GermanVocabApp.Core.SourceGeneration.Builders;
using GermanVocabApp.Core.SourceGeneration.Builders.Generators;
using System.Text;

namespace GermanVocabApp.Core.Tests.Unit.Generators;

public class ModelBuilderMemberGeneratorTests
{
    private readonly StringBuilder _sb;
    private readonly ModelBuilderMemberGenerator _memberStringBuilder;

    public ModelBuilderMemberGeneratorTests()
    {
        _sb = new StringBuilder();
        _memberStringBuilder = new ModelBuilderMemberGenerator(_sb);
    }

    [Fact]
    public void Append_ShouldAppendMemberDeclarationOnNewLine()
    {
        var propInfo = new ModelBuilderPropertyInfo("_memberName", "MemberType", "PropertyName");
        _memberStringBuilder.Append(propInfo);
        string result = _sb.ToString();
        Assert.Equal(@"    private MemberType _memberName;
", result);
    }
}
