using InfoTrack.API.Models.Data;

namespace InfoTrack.API.Interfaces.Repositories;

public interface ILocationRepository
{
    IEnumerable<Location> GetLocations();
}
