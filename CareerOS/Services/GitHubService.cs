using System.Text.Json;
using CareerOS.Models;

namespace CareerOS.Services;

public interface IGitHubService
{
    Task<List<GitHubRepo>> GetReposAsync(string username);
}

public class GitHubService : IGitHubService
{
    private readonly HttpClient _httpClient;
    public GitHubService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("CareerOSWeb/1.0");
    }

    public async Task<List<GitHubRepo>> GetReposAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username)) return [];

        try
        {
            var response = await _httpClient.GetAsync($"https://api.github.com/users/{username}/repos?sort=updated");
            if (!response.IsSuccessStatusCode) return [];

            await using var stream = await response.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);
            return doc.RootElement.EnumerateArray().Select(x => new GitHubRepo
            {
                Name = x.GetProperty("name").GetString() ?? string.Empty,
                Description = x.GetProperty("description").GetString() ?? "Sem descrição",
                Language = x.TryGetProperty("language", out var l) ? l.GetString() ?? "N/A" : "N/A",
                Stars = x.GetProperty("stargazers_count").GetInt32(),
                UpdatedAt = x.GetProperty("updated_at").GetDateTime(),
                Url = x.GetProperty("html_url").GetString() ?? string.Empty
            }).OrderByDescending(x => x.Stars).ThenByDescending(x => x.UpdatedAt).ToList();
        }
        catch
        {
            return [];
        }
    }
}
