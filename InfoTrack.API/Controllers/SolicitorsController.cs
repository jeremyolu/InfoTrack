using Microsoft.AspNetCore.Mvc;

namespace InfoTrack.API.Controllers;

[ApiController]
[Route("solicitors")]
public class SolicitorsController : ControllerBase
{
    private readonly ILogger<SolicitorsController> _logger;

    public SolicitorsController(ILogger<SolicitorsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok("Test");
    }
}
