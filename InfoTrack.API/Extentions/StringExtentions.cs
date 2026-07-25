namespace InfoTrack.API.Extentions;

public static class StringExtentions
{
    public static string Capitalize(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return value;

        return char.ToUpper(value[0]) + value.Substring(1);
    }
}
