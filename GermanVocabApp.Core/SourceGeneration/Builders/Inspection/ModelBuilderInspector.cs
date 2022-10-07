using System.Reflection;

namespace GermanVocabApp.Core.SourceGeneration.Builders.Inspection;

public class ModelBuilderInspector
{
    private readonly ModelBuilderPropertyInspector _propertyInspector;

    public ModelBuilderInspector(ModelBuilderPropertyInspector propertyInspector)
    {
        _propertyInspector = propertyInspector;
    }

    public ModelBuilderInfo Inspect(Type modelType)
    {
        ModelBuilderPropertyInfo[] properties;
        properties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                              .Where(p => p.CanWrite)
                              .OrderBy(p => p.Name)
                              .Select(p => _propertyInspector.Inspect(p))
                              .ToArray();

        return new ModelBuilderInfo($"{modelType.Name}Builder", modelType.Name, properties);
    }
}