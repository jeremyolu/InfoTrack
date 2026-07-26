using InfoTrack.API.Interfaces.Repositories;
using InfoTrack.API.Interfaces.Services;
using InfoTrack.API.Models.Requests;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace InfoTrack.API.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ClaimsIdentity?> SetUserAsync(LoginRequest loginRequest)
    {
        if (string.IsNullOrWhiteSpace(loginRequest.Username) || string.IsNullOrWhiteSpace(loginRequest.Password))
            return null;

        try
        {
            ClaimsIdentity? claimsIdentity = null;

            var user = _userRepository.GetUser(loginRequest.Username, loginRequest.Password);

            if (user == null)
                return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        }
        catch (Exception) { }

        return await Task.FromResult(claimsIdentity);
    }
}
