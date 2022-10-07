using Osiris.System.Extensions;
using System.Reflection;

namespace GermanVocabApp.Core.SourceGeneration.Builders.Inspection;

public class ModelBuilderPropertyInspector
{
    private readonly Dictionary<string, string> _buildInTypeDict;
    private readonly NullableReferenceTypeInspector _nullReferenceTypeInpsector;

    public ModelBuilderPropertyInspector(IBuiltInTypeDictProvider builIBuiltInDictProvider,
        NullableReferenceTypeInspector nullReferenceTypeInpsector)
    {
        _buildInTypeDict = builIBuiltInDictProvider.Provide();
        _nullReferenceTypeInpsector = nullReferenceTypeInpsector;
    }

    public ModelBuilderPropertyInfo Inspect(PropertyInfo propertyInfo)
    {
        Tuple<bool, string> nullableTestResult = TryGetNullableUnderlyingTypeName(propertyInfo);
        bool isNullable = nullableTestResult.Item1;
        string dotNetTypeName = nullableTestResult.Item2;

        string propertyTypeName = TryGetKeywordTypeName(dotNetTypeName);
        if (isNullable)
        {
            propertyTypeName = $"{propertyTypeName}?";
        }

        return new ModelBuilderPropertyInfo(StringTransformationExtensions.ToPrivateMemberName(propertyInfo.Name),
                                            propertyTypeName, propertyInfo.Name);
    }

    private Tuple<bool, string> TryGetNullableUnderlyingTypeName(PropertyInfo propertyInfo)
    {
        string dotNetTypeName;
        bool isNullable;

        Type propertyType = propertyInfo.PropertyType;

        Type? underlyingNullableValueType = Nullable.GetUnderlyingType(propertyType);
        if (underlyingNullableValueType != null)
        {
            dotNetTypeName = underlyingNullableValueType.Name;
            isNullable = true;
        }
        else
        {
            dotNetTypeName = propertyInfo.PropertyType.Name;
            isNullable = _nullReferenceTypeInpsector.AssertIsNullable(propertyInfo);
        }
        return new Tuple<bool, string>(isNullable, dotNetTypeName);
    }

    private string TryGetKeywordTypeName(string dotNetTypeName)
    {
        return (_buildInTypeDict.ContainsKey(dotNetTypeName))
               ? _buildInTypeDict[dotNetTypeName]
               : dotNetTypeName;
    }
}