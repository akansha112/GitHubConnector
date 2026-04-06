using GitHubConnector.Domain.Models;

namespace GitHubConnector.Application.Interfaces;

public interface IGitHubService
{
    Task<List<Repository>> GetRepositories(string accessToken);
    Task CreateIssue(string accessToken, string owner, string repo, Issue issue);
}