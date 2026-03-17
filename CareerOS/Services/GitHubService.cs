using System.Net.Http.Headers;
using System.Text.Json;
using CareerOS.Models;

namespace CareerOS.Services;

public class GitHubService
{
    private readonly HttpClient _httpClient = new();

    public GitHubService()
    {
        _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("CareerOS", "1.0"));
    }

    public async Task<List<GitHubRepo>> GetReposAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username)) return new();

        var response = await _httpClient.GetAsync($"https://api.github.com/users/{username}/repos?sort=updated");
        if (!response.IsSuccessStatusCode)
        {
            return new();
        }

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var repos = new List<GitHubRepo>();

        foreach (var item in doc.RootElement.EnumerateArray())
        {
            repos.Add(new GitHubRepo
            {
                Name = item.GetProperty("name").GetString() ?? string.Empty,
                Description = item.GetProperty("description").GetString() ?? "Sem descrição",
                Language = item.TryGetProperty("language", out var lang) ? lang.GetString() ?? "N/A" : "N/A",
                Stars = item.GetProperty("stargazers_count").GetInt32(),
                UpdatedAt = item.GetProperty("updated_at").GetDateTime(),
                HtmlUrl = item.GetProperty("html_url").GetString() ?? string.Empty
            });
        }

        return repos.OrderByDescending(r => r.Stars).ThenByDescending(r => r.UpdatedAt).ToList();
    }
}
