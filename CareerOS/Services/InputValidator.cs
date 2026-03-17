using System.Text.RegularExpressions;

namespace CareerOS.Services;

public static partial class InputValidator
{
    [GeneratedRegex("^[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,37})$")]
    private static partial Regex GitHubRegex();

    public static bool IsValidGitHubUsername(string username)
        => !string.IsNullOrWhiteSpace(username) && GitHubRegex().IsMatch(username.Trim());

    public static string NormalizeGitHubUsername(string username)
        => username.Trim();

    public static string Safe(string? value, int max = 250)
    {
        if (string.IsNullOrWhiteSpace(value)) return string.Empty;
        var text = value.Trim();
        return text.Length <= max ? text : text[..max];
    }
}
