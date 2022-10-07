using System.Reflection;

namespace GermanVocabApp.Core.SourceGeneration.Builders.Inspection;

public class NullableReferenceTypeInspector
{
    public bool AssertIsNullable(PropertyInfo propertyInfo)
    {
        NullabilityInfoContext nullInfoContext = new NullabilityInfoContext();
        NullabilityInfo nullInfo = nullInfoContext.Create(propertyInfo);
        NullabilityState nullabilityState = nullInfo.WriteState;
        return nullabilityState == NullabilityState.Nullable;
    }
}


