using InfoTrack.API.Interfaces.Repositories;
using InfoTrack.API.Models.Data;

namespace InfoTrack.API.Repositories;

public class UserRepository : IUserRepository
{
    public User? GetUser(string username, string password)
    {
        return GetUsers().FirstOrDefault(x => x.Username == username && x.Password == password);
    }

    private IEnumerable<User> GetUsers()
    {
        return new List<User>
        {
            new User { Id = 1001, Username = "test.user.a", Password = "Password123*" },
            new User { Id = 1002, Username = "test.user.b", Password = "PasswordAbc!" },
        };
    }
}
