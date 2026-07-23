using InfoTrack.API.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrack.API.Controllers;

[ApiController]
[Route("api/locations")]
public class LocationsController : ControllerBase
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

        return Ok(response);
    }
}
