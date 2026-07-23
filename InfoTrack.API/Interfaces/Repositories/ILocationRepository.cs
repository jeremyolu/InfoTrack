using InfoTrack.API.Models;

namespace InfoTrack.API.Interfaces.Repositories;

public interface ILocationRepository
{
    IEnumerable<Location> GetLocations();
}
