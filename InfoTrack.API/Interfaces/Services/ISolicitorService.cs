using InfoTrack.API.Models.Responses;

namespace InfoTrack.API.Interfaces.Services;

public interface ISolicitorService
{
    Task<ResultsResponse<SolicitorResponse>> GetSolicitorsByLocationAsync(string location, string? sortBy);
}
