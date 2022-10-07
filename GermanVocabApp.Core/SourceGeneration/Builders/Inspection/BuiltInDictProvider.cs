namespace GermanVocabApp.Core.SourceGeneration.Builders.Inspection;

public class BuiltInTypeDictProvider : IBuiltInTypeDictProvider
{
    public Dictionary<string, string> Provide()
    {
        return new Dictionary<string, string>
        {
            { "Boolean", "bool"},
            { "Byte", "byte"},
            { "Char", "char"},
            { "Decimal", "decimal"},
            { "Double", "double"},
            { "Int16", "short"},
            { "Int32", "int"},
            { "Int64", "long"},
            { "IntPtr", "nint"},
            { "SByte", "sbyte"},
            { "Single", "float"},
            { "UInt16", "ushort"},
            { "UInt32", "uint"},
            { "UInt64", "ulong"},
            { "UIntPtr", "nuint"},
        };
    }
}

