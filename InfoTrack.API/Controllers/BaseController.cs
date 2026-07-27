using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace InfoTrack.API.Controllers;

public class BaseController : ControllerBase
{
    protected string? GetUserId()
    {
        return User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    protected IActionResult SetResponseCode(HttpStatusCode statusCode, object? data = null)
    {
        return StatusCode((int)statusCode, data);
    }
}
