namespace Altin.Shared.Helpers;

public static class StringHelper
{
    public static string NormalizeString(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var normalizedString = input
            .Replace("ç", "c")
            .Replace("Ç", "C")
            .Replace("ğ", "g")
            .Replace("Ğ", "G")
            .Replace("ı", "i")
            .Replace("İ", "I")
            .Replace("ö", "o")
            .Replace("Ö", "O")
            .Replace("ş", "s")
            .Replace("Ş", "S")
            .Replace("ü", "u")
            .Replace("Ü", "U");

        normalizedString = normalizedString.ToLowerInvariant().Replace(" ", "_");

        return normalizedString;
    }
}