﻿#r "..\..\GermanVocabApp.DataAccess.EntityFramework\bin\Debug\net6.0\GermanVocabApp.DataAccess.EntityFramework.dll"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

Output.BuildAction = BuildAction.None;
Output.SetExtension(".cs");
Output.WriteLine($@"// ########## Auto-Generated Code ##########
// <auto-generated>
// This code was automatically generated by a tool.
//
// Changes to this file may cause incorrect behaviour, and the changes will be lost if
// the code is regenerated.
// </auto-generated>
// ##########################################

using System;
using System.CodeDom.Compiler;
using GermanVocabApp.DataAccess.EntityFramework.Models;
using GermanVocabApp.Shared.Data;

namespace GermanVocabApp.DataAccess.EntityFramework.Tests.Unit.Builders;

{BuildTypes()} 

");



private string BuildTypes()
{
    StringBuilder sb = new StringBuilder();

    try
    {
        Type modelType = typeof(GermanVocabApp.DataAccess.EntityFramework.Models.EntityBase);
        Assembly modelAssembly = modelType.Assembly;
        IEnumerable<Type> assemblyTypes;
        try
        {
            assemblyTypes = modelAssembly.DefinedTypes
                                         .Select(ti => ti.AsType());
        }
        catch (ReflectionTypeLoadException re)
        {
            assemblyTypes = re.Types
                              .Where(t => t != null);
        }

        Type[] modelTypes = assemblyTypes.Where(t => !t.IsGenericTypeDefinition
                                                  && !t.IsAbstract
                                                  && modelType.IsAssignableFrom(t))
                                         .OrderBy(t => t.Name)
                                         .ToArray();

        for (int i = 0; i < modelTypes.Length; i++)
        {
            Type type = modelTypes[i];
            GeneratePartialClass(sb, type);
        }

        return sb.ToString();
    }
    catch (Exception e)
    {
        return $"// {e.ToString()}";
    }
}

private void GeneratePartialClass(StringBuilder sb, Type type)
{
    string typeName = type.Name;
    string typeFullName = type.FullName;
    string builderTypeName = $"{typeName}Builder";

    PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                    .Where(p => p.CanWrite)
                                    .OrderBy(p => p.Name)
                                    .ToArray();

    sb.AppendLine($@"
[GeneratedCode(""Builders"", ""1.0"")]
public partial class {builderTypeName} : BuilderBase<{typeFullName}>
{{
{WriteClassBody(properties, builderTypeName, typeName)}
}}");
}

private string WriteClassBody(PropertyInfo[] propertyInfos, string builderTypeName, string typeName)
{
    StringBuilder sb = new StringBuilder();

    for (int i = 0; i < propertyInfos.Length; i++)
    {
        PropertyInfo propertyInfo = propertyInfos[i];
        BuildPropertyAndMethod(sb, propertyInfo, builderTypeName);
    }

    AppendBuildMethod(sb, typeName, propertyInfos);
    return sb.ToString();
}

private void BuildPropertyAndMethod(StringBuilder sb, PropertyInfo propertyInfo, string builderTypeName)
{
    string propertyName = propertyInfo.Name;
    string propertyTypeName = "object";

    try
    {
        if (propertyInfo.PropertyType.Name == "IEnumerable`1")
        {
            sb.AppendLine($@"    //{propertyName};");
        }

        propertyTypeName = propertyInfo.PropertyType.GetValidTypeName();
        string memberName = $@"_{CamelCase(propertyName)}";
        AppendMemberDeclaration(sb, propertyTypeName, memberName);
        AppendMethodDeclaration(sb, builderTypeName, propertyTypeName, propertyName, memberName);
    }
    catch (Exception e)
    {
        sb.Append($"Error occurred while examining property {propertyName}: {e.Message}");
    }
}

private static string GetValidTypeName(this Type type)
{
    Type underlyingType = Nullable.GetUnderlyingType(type);
    if (type.Name == "Nullable`1")
    {
        return underlyingType.Name.ToCompilableTypeNameQuick() + "?";
    }

    return type.Name.ToCompilableTypeNameQuick();
}

private static string ToCompilableTypeNameQuick(this string typeName)
{
    Tuple<bool, string> primitiveCheckResult = TryGetPrimitiveTypeName(typeName);
    if (primitiveCheckResult.Item1 == true && primitiveCheckResult.Item2 == null)
    {
        throw new NullReferenceException("Primitive type check was successful, but the returned type name was null");
    }
    else if (primitiveCheckResult.Item1 == false && primitiveCheckResult.Item2 != null)
    {
        return primitiveCheckResult.Item2;
    }

    StringBuilder sb = new StringBuilder();

    bool potentialAngleBrace = false;

    for (int i = 0; i < typeName.Length; i++)
    {
        char c = typeName[i];

        if (c == '`' && !potentialAngleBrace)
        {
            potentialAngleBrace = true;
            continue;
        }
        else
        {
            potentialAngleBrace = false;
        }

        if (c == '1' && potentialAngleBrace)
        {
            continue;
        }
        else
        {
            potentialAngleBrace = false;
        }

        if (c == '[' && potentialAngleBrace)
        {
            sb.Append('<');
            potentialAngleBrace = false;
            continue;
        }
        else
        {
            potentialAngleBrace = false;
        }

        if (c == ']')
        {
            sb.Append('>');
            continue;
        }

        sb.Append(c);
    }
    return sb.ToString();
}

private static string CamelCase(string value)
{
    return $"{value.Substring(0, 1).ToLowerInvariant()}{value.Substring(1)}";
}

private static Tuple<bool, string> TryGetPrimitiveTypeName(string typeName)
{
    if (typeName == "String")
    {
        return new Tuple<bool, string>(true, "string");
    }

    if (typeName == "Int32")
    {
        return new Tuple<bool, string>(true, "int");
    }

    if (typeName == "Boolean")
    {
        return new Tuple<bool, string>(true, "bool");
    }

    return new Tuple<bool, string>(false, null);
}

static void AppendMemberDeclaration(StringBuilder sb, string propertyTypeName, string memberName)
{
    sb.AppendLine($@"    private {propertyTypeName} {memberName};");
}

private static void AppendMethodDeclaration(StringBuilder sb, string builderTypeName, string propertyTypeName, string propertyName, string memberName)
{
    sb.AppendLine($@"    public {builderTypeName} With{propertyName}({propertyTypeName} value)
    {{
        {memberName} = value;
        return this;
    }}");
}

private static void AppendBuildMethod(StringBuilder sb, string modelTypeName, PropertyInfo[] properties)
{
    sb.Append($@"    public override {modelTypeName} Build()
    {{
        return new {modelTypeName}
        {{
{PropertyInitialisation(properties)}
        }};
    }}");
}

private static string PropertyInitialisation(PropertyInfo[] propertyInfos)
{
    StringBuilder sb = new StringBuilder();
    foreach (PropertyInfo prop in propertyInfos)
    {
        string propertyName = prop.Name;
        string memberName = $@"_{CamelCase(propertyName)}";

        sb.AppendLine($"            {propertyName} = {memberName},");
    }
    return sb.ToString();
}