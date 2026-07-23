using InfoTrack.API.Models;
using InfoTrack.API.Models.Responses;

namespace InfoTrack.API.Interfaces.Services;

public interface ILocationService
{
    Task<ResultsResponse<Location>> GetLocations();
}
