using GitHubConnector.Application.Interfaces;
using GitHubConnector.Domain.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GitHubConnector.Application.Services;

public class GitHubService : IGitHubService
{
    private readonly HttpClient _httpClient;

    public GitHubService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Repository>> GetRepositories(string accessToken)
    {
        _httpClient.DefaultRequestHeaders.Clear();

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);

        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("DotNetApp");

        var response = await _httpClient.GetAsync("https://api.github.com/user/repos");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var repos = JsonSerializer.Deserialize<List<JsonElement>>(json);

        return repos.Select(r => new Repository
        {
            Name = r.GetProperty("name").GetString(),
            Url = r.GetProperty("html_url").GetString()
        }).ToList();
    }

    public async Task CreateIssue(string accessToken, string owner, string repo, Issue issue)
    {
        _httpClient.DefaultRequestHeaders.Clear();

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);

        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("DotNetApp");

        var content = new StringContent(
            JsonSerializer.Serialize(issue),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync(
            $"https://api.github.com/repos/{owner}/{repo}/issues",
            content);

        response.EnsureSuccessStatusCode();
    }
}