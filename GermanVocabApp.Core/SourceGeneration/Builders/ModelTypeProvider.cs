using GermanVocabApp.DataAccess.Shared;
using System.Reflection;

namespace GermanVocabApp.Core.SourceGeneration.Builders;

public class ModelTypeProvider<TBase>
    where TBase : class
{
    public Type[] Provide()
    {
        Type modelType = typeof(EntityBase);
        Assembly modelAssembly = modelType.Assembly;
        
        IEnumerable<Type> assemblyTypes;
        try
        {
            assemblyTypes = modelAssembly.DefinedTypes
                                         .Select(ti => ti.AsType());
        }
        catch (ReflectionTypeLoadException re)
        {
#pragma warning disable CS8619
            assemblyTypes = re.Types
                              .Where(t => t != null);
#pragma warning restore CS8619
        }

        return assemblyTypes.Where(t => !t.IsGenericTypeDefinition
                                     && !t.IsAbstract
                                     && modelType.IsAssignableFrom(t))
                            .OrderBy(t => t.Name)
                            .ToArray();

    }
}