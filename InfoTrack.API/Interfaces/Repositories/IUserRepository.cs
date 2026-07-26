using InfoTrack.API.Models.Data;

namespace InfoTrack.API.Interfaces.Repositories;

public interface IUserRepository
{
    User? GetUser(string username, string password);
}
