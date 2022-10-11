﻿//#r "..\..\GermanVocabApp.DataAccess.EntityFramework\bin\Debug\net6.0\GermanVocabApp.DataAccess.EntityFramework.dll"
#r "..\..\bin\Debug\net6.0\GermanVocabApp.Core.dll"
//#r "..\bin\Debug\net6.0\GermanVocabApp.DataAccess.EntityFramework.Tests.Unit.dll"

using System;
using System.Text;
using System.Reflection;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Collections.Generic;

using GermanVocabApp.Core.SourceGeneration;
using GermanVocabApp.Core.SourceGeneration.Builders;
using GermanVocabApp.Core.SourceGeneration.Builders.Generators;
using GermanVocabApp.Core.SourceGeneration.Builders.Inspection;
//using GermanVocabApp.DataAccess.EntityFramework.Models;
//using GermanVocabApp.DataAccess.EntityFramework.Tests.Unit.Builders;
using GermanVocabApp.DataAccess.Shared.Abstractions;
using GermanVocabApp.Shared.Data;
using GermanVocabApp.Shared;

Output.BuildAction = BuildAction.None;
Output.SetExtension(".cs");
Output.WriteLine($@"// <auto-generated>
using System;
using System.Runtime;
using System.CodeDom.Compiler;
using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.Core.SourceGeneration.Builders;

{TestStuff()}
");


private string TestStuff()
{
    try
    {
        IBuiltInTypeDictProvider typeDictProvider = new BuiltInTypeDictProvider();
        NullableReferenceTypeInspector nullReferenceTypeInpsector = new NullableReferenceTypeInspector();
        ModelBuilderPropertyInspector propertyInspector = new ModelBuilderPropertyInspector(typeDictProvider, nullReferenceTypeInpsector);

        Type type = typeof(VocabListItem);
        PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .Where(p => p.CanWrite)
                                 .OrderBy(p => p.Name)
                                 .ToArray();
        //var propOne = props[0];
        //ModelBuilderPropertyInfo builderPropInfo = propertyInspector.Inspect(props[0]);
        ModelBuilderPropertyInfo builderPropInfo = new ModelBuilderPropertyInfo("_wordType", "WordType", "WordType");

        StringBuilder sb = new StringBuilder();
        ModelBuilderMemberGenerator generator = new ModelBuilderMemberGenerator(sb);
        generator.Append(builderPropInfo);
        return sb.ToString();
    }
    catch (Exception e)
    {
        return $@"/*
{e.ToString()}
*/";
    }
}

public void New()
{

}

//private string BuildTypes()
//{sdsd
//    StringBuilder sb = new StringBuilder();
//    try
//    {
//        Type modelType = typeof(GermanVocabApp.DataAccess.EntityFramework.Models.EntityBase);
//        Assembly modelAssembly = modelType.Assembly;
//        IEnumerable<Type> assemblyTypes;
//        try
//        {
//            assemblyTypes = modelAssembly.DefinedTypes
//                                         .Select(ti => ti.AsType());
//        }
//        catch (ReflectionTypeLoadException re)
//        {
//            assemblyTypes = re.Types
//                              .Where(t => t != null);
//        }

//        Type[] modelTypes = assemblyTypes.Where(t => !t.IsGenericTypeDefinition
//                                                  && !t.IsAbstract
//                                                  && modelType.IsAssignableFrom(t))
//                                         .OrderBy(t => t.Name)
//                                         .ToArray();

//        for (int i = 0; i < modelTypes.Length; i++)
//        {
//            Type type = modelTypes[i];
//            GeneratePartialClass(sb, type);
//        }

//        return sb.ToString();
//    }
//    catch (Exception e)
//    {
//        return $"// {e.ToString()}";
//    }
//}

//private void GeneratePartialClass(StringBuilder sb, Type type)
//{
//    string typeName = type.Name;
//    string typeFullName = type.FullName;
//    string builderTypeName = $"{typeName}Builder";

//    PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
//                                    .Where(p => p.CanWrite)
//                                    .OrderBy(p => p.Name)
//                                    .ToArray();

//    sb.AppendLine($@"
//[GeneratedCode(""Builders"", ""1.0"")]
//public partial class {builderTypeName} : BuilderBase<{typeFullName}>
//{{
//{WriteClassBody(properties, builderTypeName, typeName)}
//}}");
//}

//private string WriteClassBody(PropertyInfo[] propertyInfos, string builderTypeName, string typeName)
//{
//    StringBuilder sb = new StringBuilder();

//    for (int i = 0; i < propertyInfos.Length; i++)
//    {
//        PropertyInfo propertyInfo = propertyInfos[i];
//        BuildPropertyAndMethod(sb, propertyInfo, builderTypeName);
//    }

//    AppendBuildMethod(sb, typeName, propertyInfos);
//    return sb.ToString();
//}

//private void BuildPropertyAndMethod(StringBuilder sb, PropertyInfo propertyInfo, string builderTypeName)
//{
//    string propertyName = propertyInfo.Name;
//    string propertyTypeName = "object";

//    try
//    {
//        if (propertyInfo.PropertyType.Name == "IEnumerable`1")
//        {
//            sb.AppendLine($@"    //{propertyName};");
//        }

//        propertyTypeName = propertyInfo.PropertyType.GetValidTypeName();
//        string memberName = $@"_{CamelCase(propertyName)}";
//        AppendMemberDeclaration(sb, propertyTypeName, memberName);
//        AppendMethodDeclaration(sb, builderTypeName, propertyTypeName, propertyName, memberName);
//    }
//    catch (Exception e)
//    {
//        sb.Append($"Error occurred while examining property {propertyName}: {e.Message}");
//    }
//}

//private static string GetValidTypeName(this Type type)
//{
//    Type underlyingType = Nullable.GetUnderlyingType(type);
//    if (type.Name == "Nullable`1")
//    {
//        return underlyingType.Name.ToCompilableTypeNameQuick() + "?";
//    }

//    return type.Name.ToCompilableTypeNameQuick();
//}

//private static string ToCompilableTypeNameQuick(this string typeName)
//{
//    Tuple<bool, string> primitiveCheckResult = TryGetPrimitiveTypeName(typeName);
//    if (primitiveCheckResult.Item1 == true && primitiveCheckResult.Item2 == null)
//    {
//        throw new NullReferenceException("Primitive type check was successful, but the returned type name was null");
//    }
//    else if (primitiveCheckResult.Item1 == false && primitiveCheckResult.Item2 != null)
//    {
//        return primitiveCheckResult.Item2;
//    }

//    StringBuilder sb = new StringBuilder();

//    bool potentialAngleBrace = false;

//    for (int i = 0; i < typeName.Length; i++)
//    {
//        char c = typeName[i];

//        if (c == '`' && !potentialAngleBrace)
//        {
//            potentialAngleBrace = true;
//            continue;
//        }
//        else
//        {
//            potentialAngleBrace = false;
//        }

//        if (c == '1' && potentialAngleBrace)
//        {
//            continue;
//        }
//        else
//        {
//            potentialAngleBrace = false;
//        }

//        if (c == '[' && potentialAngleBrace)
//        {
//            sb.Append('<');
//            potentialAngleBrace = false;
//            continue;
//        }
//        else
//        {
//            potentialAngleBrace = false;
//        }

//        if (c == ']')
//        {
//            sb.Append('>');
//            continue;
//        }

//        sb.Append(c);
//    }
//    return sb.ToString();
//}

//private static string CamelCase(string value)
//{
//    return $"{value.Substring(0, 1).ToLowerInvariant()}{value.Substring(1)}";
//}

//private static Tuple<bool, string> TryGetPrimitiveTypeName(string typeName)
//{
//    if (typeName == "String")
//    {
//        return new Tuple<bool, string>(true, "string");
//    }

//    if (typeName == "Int32")
//    {
//        return new Tuple<bool, string>(true, "int");
//    }

//    if (typeName == "Boolean")
//    {
//        return new Tuple<bool, string>(true, "bool");
//    }

//    return new Tuple<bool, string>(false, null);
//}

//static void AppendMemberDeclaration(StringBuilder sb, string propertyTypeName, string memberName)
//{
//    sb.AppendLine($@"    private {propertyTypeName} {memberName};");
//}

//private static void AppendMethodDeclaration(StringBuilder sb, string builderTypeName, string propertyTypeName, string propertyName, string memberName)
//{
//    sb.AppendLine($@"    public {builderTypeName} With{propertyName}({propertyTypeName} value)
//    {{
//        {memberName} = value;
//        return this;
//    }}");
//}

//private static void AppendBuildMethod(StringBuilder sb, string modelTypeName, PropertyInfo[] properties)
//{
//    sb.Append($@"    public override {modelTypeName} Build()
//    {{
//        return new {modelTypeName}
//        {{
//{PropertyInitialisation(properties)}
//        }};
//    }}");
//}

//private static string PropertyInitialisation(PropertyInfo[] propertyInfos)
//{
//    StringBuilder sb = new StringBuilder();
//    foreach (PropertyInfo prop in propertyInfos)
//    {
//        string propertyName = prop.Name;
//        string memberName = $@"_{CamelCase(propertyName)}";

//        sb.AppendLine($"            {propertyName} = {memberName},");
//    }
//    return sb.ToString();
//}

public class VocabListItem
{
    public WordType WordType { get; set; }
    //public bool? IsWeakMasculineNoun { get; set; }
    //public ReflexiveCase? ReflexiveCase { get; set; }
    //public Separability? Separability { get; set; }
    //public Transitivity? Transitivity { get; set; }
    //public string? ThirdPersonPresent { get; set; }
    //public string? ThirdPersonImperfect { get; set; }
    //public AuxiliaryVerb? AuxiliaryVerb { get; set; }
    //public string? Perfect { get; set; }
    //public Gender? Gender { get; set; }
    //public string German { get; set; }
    //public string? Plural { get; set; }
    //public string? Preposition { get; set; }
    //public Case? PrepositionCase { get; set; }
    //public string? Comparative { get; set; }
    //public string? Superlative { get; set; }
    //public string English { get; set; }
    //public Guid VocabListId { get; set; }
    //public virtual VocabList VocabList { get; set; }
    //public FixedPlurality? FixedPlurality { get; set; }
}