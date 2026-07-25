using InfoTrack.API.Interfaces.Repositories;
using InfoTrack.API.Models.Data;

namespace InfoTrack.API.Repositories;

public class LocationStaticRepository : ILocationRepository
{
    public IEnumerable<Location> GetLocations()
    {
        return new List<Location>
        {
            new Location { Name = "London" },
            new Location { Name = "Birmingham" },
            new Location { Name = "Leeds" },
            new Location { Name = "Manchester" },
            new Location { Name = "Sheffield" },
            new Location { Name = "Bradford" },
            new Location { Name = "Liverpool" },
            new Location { Name = "Sheffield" }
        };
    }
}