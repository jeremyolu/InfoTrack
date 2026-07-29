using InfoTrack.API.Interfaces.Repositories;
using InfoTrack.API.Interfaces.Services;
using InfoTrack.API.Models.Data;
using InfoTrack.API.Models.Responses;
using System.Net;

namespace InfoTrack.API.Services;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;

    public LocationService(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<ResultsResponse<Location>> GetLocations()
    {
        var response = new ResultsResponse<Location>();

        try
        {
            var locations = _locationRepository.GetLocations();

            if (locations == null || !locations.Any())
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = $"Unable to retrive locations at the moment.";

                return response;
            }

            response.Count = locations.Count();
            response.StatusCode = HttpStatusCode.OK;
            response.Results = locations;
        }
        catch (Exception ex)
        {
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Message = !string.IsNullOrWhiteSpace(ex.InnerException?.Message) ? ex.InnerException.Message : ex.Message;
        }

        return await Task.FromResult(response);
    }
}
