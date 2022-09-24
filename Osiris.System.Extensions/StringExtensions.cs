namespace Osiris.System.Extensions;
public static class StringExtensions
{
    public static bool IsNullEmptyOrWhiteSpace(this string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return true;
        }

        int charCount = value.Length;
        for (int i = 0; i < charCount; i++)
        {
            if (!char.IsWhiteSpace(value[i]))
            {
                return false;
            }
        }
        return true;
    }
}
