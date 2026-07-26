using InfoTrack.API.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrack.API.Controllers;

[Authorize]
[ApiController]
[Route("infotrack/api/locations")]
public class LocationsController : BaseController
{
    private readonly ILogger<LocationsController> _logger;
    private readonly ILocationService _locationService;

    public LocationsController(ILogger<LocationsController> logger, ILocationService locationService)
    {
        _logger = logger;
        _locationService = locationService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _locationService.GetLocations();

        return SetResponseCode(response.StatusCode, response);
    }
}
