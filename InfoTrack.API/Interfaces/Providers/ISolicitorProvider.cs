using InfoTrack.API.Models.Data;

namespace InfoTrack.API.Interfaces.Providers;

public interface ISolicitorProvider
{
    Task<IEnumerable<Solicitor>> GetSolicitorsByLocationAsync(string location);
}
