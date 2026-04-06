using System.Text.Json;

namespace GitHubConnector.Infrastructure.Auth;

public class GitHubOAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public GitHubOAuthService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public string GetLoginUrl()
    {
        var clientId = _config["GitHub:ClientId"];
        var redirectUri = "https://localhost:7253/api/auth/callback";

        return $"https://github.com/login/oauth/authorize?client_id={clientId}&redirect_uri={redirectUri}&scope=repo";
    }

    public async Task<string> GetAccessToken(string code)
    {
        var clientId = _config["GitHub:ClientId"];
        var clientSecret = _config["GitHub:ClientSecret"];
        var redirectUri = "https://localhost:7253/api/auth/callback";

        var values = new Dictionary<string, string>
        {
            { "client_id", clientId },
            { "client_secret", clientSecret },
            { "code", code },
            { "redirect_uri", redirectUri }
        };

        var content = new FormUrlEncodedContent(values);

        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        var response = await _httpClient.PostAsync(
            "https://github.com/login/oauth/access_token",
            content);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var tokenObj = JsonSerializer.Deserialize<JsonElement>(json);

        return tokenObj.GetProperty("access_token").GetString();
    }
}