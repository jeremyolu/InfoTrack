using InfoTrack.API.Models.Requests;
using InfoTrack.API.Models.Responses;
using System.Security.Claims;

namespace InfoTrack.API.Interfaces.Services;

public interface IAuthService
{
    Task<ResultResponse<AccountResponse>?> GetUserAccountAsync(string? id);
    Task<ClaimsIdentity?> AuthenticateUserAsync(LoginRequest loginRequest);
}
