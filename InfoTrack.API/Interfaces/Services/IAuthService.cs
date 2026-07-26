using InfoTrack.API.Models.Requests;
using System.Security.Claims;

namespace InfoTrack.API.Interfaces.Services;

public interface IAuthService
{
    Task<ClaimsIdentity?> SetUserAsync(LoginRequest loginRequest);
}
