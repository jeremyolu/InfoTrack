using InfoTrack.API.Models.Data;
using InfoTrack.API.Models.Responses;

namespace InfoTrack.API.Interfaces.Services;

public interface ILocationService
{
    Task<ResultsResponse<Location>> GetLocations();
}
