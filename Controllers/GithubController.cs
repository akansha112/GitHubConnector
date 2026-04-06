using GitHubConnector.Application.Interfaces;
using GitHubConnector.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GitHubConnector.Controllers;

[ApiController]
[Route("api/github")]
public class GitHubController : ControllerBase
{
    private readonly IGitHubService _gitHubService;

    public GitHubController(IGitHubService gitHubService)
    {
        _gitHubService = gitHubService;
    }

    [HttpGet("repos")]
    public async Task<IActionResult> GetRepos([FromHeader] string accessToken)
    {
        var repos = await _gitHubService.GetRepositories(accessToken);
        return Ok(repos);
    }

    [HttpPost("issue")]
    public async Task<IActionResult> CreateIssue(
        [FromHeader] string accessToken,
        string owner,
        string repo,
        Issue issue)
    {
        await _gitHubService.CreateIssue(accessToken, owner, repo, issue);
        return Ok("Issue Created");
    }
}