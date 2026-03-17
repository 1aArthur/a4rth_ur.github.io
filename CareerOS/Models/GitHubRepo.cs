namespace CareerOS.Models;

public class GitHubRepo
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public int Stars { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string HtmlUrl { get; set; } = string.Empty;
}
