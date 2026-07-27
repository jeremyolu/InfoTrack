using InfoTrack.API.Interfaces.Services;
using InfoTrack.API.Models.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace InfoTrack.API.Controllers;

[ApiController]
[Route("infotrack/api/auth")]
public class AuthController : BaseController
{
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;

    public AuthController(ILogger<AuthController> logger, IAuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var identity = await _authService.AuthenticateUserAsync(loginRequest);

        if (identity == null)
            return SetResponseCode(HttpStatusCode.Unauthorized);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

        return SetResponseCode(HttpStatusCode.OK);
    }

    [HttpGet("account")]
    [Authorize]
    public async Task<IActionResult> Account()
    {
        var response = await _authService.GetUserAccountAsync(GetUserId());

        return SetResponseCode(response.StatusCode, response); ;
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        return Ok();
    }
}
