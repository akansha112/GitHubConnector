using GitHubConnector.Infrastructure.Auth;
using Microsoft.AspNetCore.Mvc;

namespace GitHubConnector.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly GitHubOAuthService _oauthService;

    public AuthController(GitHubOAuthService oauthService)
    {
        _oauthService = oauthService;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        var url = _oauthService.GetLoginUrl();
        return Redirect(url);
    }

    [HttpGet("callback")]
    public async Task<IActionResult> Callback(string code)
    {
        var token = await _oauthService.GetAccessToken(code);
        return Ok(token);
    }
}