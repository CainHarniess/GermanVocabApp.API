using GermanVocabApp.Core.SourceGeneration.Builders;

namespace GermanVocabApp.Core.Tests.Unit.Generators;

public static class ModelBuilderTestDataUtilities
{
    public static ModelBuilderPropertyInfo[] BuildPropertyInfoArray()
    {
        var prop1Info = new ModelBuilderPropertyInfo("_memberOneName", "MemberOneType", "PropertyOneName");
        var prop2Info = new ModelBuilderPropertyInfo("_memberTwoName", "MemberTwoType", "PropertyTwoName");
        var prop3Info = new ModelBuilderPropertyInfo("_memberThreeName", "MemberThreeType", "PropertyThreeName");
        ModelBuilderPropertyInfo[] props = { prop1Info, prop2Info, prop3Info };
        return props;
    }
}
