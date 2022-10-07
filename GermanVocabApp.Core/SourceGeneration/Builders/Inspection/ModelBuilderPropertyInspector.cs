using Osiris.System.Extensions;
using System.Reflection;

namespace GermanVocabApp.Core.SourceGeneration.Builders.Inspection;

public class ModelBuilderPropertyInspector
{
    public ModelBuilderPropertyInfo Inspect(PropertyInfo property)
    {
        return new ModelBuilderPropertyInfo(StringTransformationExtensions.ToPrivateMemberName(property.Name),
                                            property.PropertyType.Name, property.Name);
    }
}
