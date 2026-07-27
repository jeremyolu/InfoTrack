using InfoTrack.API.Models.Data;

namespace InfoTrack.API.Interfaces.Repositories;

public interface IUserRepository
{
    User? GetUser(int id);
    User? GetUser(string username);
    User? GetUserByAuth(string username, string password);
}
