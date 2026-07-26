using InfoTrack.API.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrack.API.Controllers;

[Authorize]
[ApiController]
[Route("infotrack/api/solicitors")]
public class SolicitorsController : BaseController
{
    private readonly ILogger<SolicitorsController> _logger;
    private readonly ISolicitorService _solicitorService;

    public SolicitorsController(ILogger<SolicitorsController> logger, ISolicitorService solicitorService)
    {
        _logger = logger;
        _solicitorService = solicitorService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string location, string? sortBy)
    {
        var response = await _solicitorService.GetSolicitorsByLocationAsync(location, sortBy);

        return SetResponseCode(response.StatusCode, response);
    }
}
