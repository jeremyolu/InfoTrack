using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InfoTrack.API.Controllers;

public class BaseController : ControllerBase
{
    protected IActionResult SetResponseCode(HttpStatusCode statusCode, object? data = null)
    {
        return StatusCode((int)statusCode, data);
    }
}
