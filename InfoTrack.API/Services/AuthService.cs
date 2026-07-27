using InfoTrack.API.Interfaces.Repositories;
using InfoTrack.API.Interfaces.Services;
using InfoTrack.API.Models.Requests;
using InfoTrack.API.Models.Responses;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using System.Security.Claims;

namespace InfoTrack.API.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResultResponse<AccountResponse>?> GetUserAccountAsync(string? id)
    {
        var response = new ResultResponse<AccountResponse>();

        if (string.IsNullOrWhiteSpace(id))
        {
            response.StatusCode = HttpStatusCode.Unauthorized;
            response.Message = "Unauthorized request.";

            return response;
        }

        try
        {
            int userId;

            if (!int.TryParse(id, out userId))
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Unknown error occurred.";
            }
                
            var user = _userRepository.GetUser(userId);

            if (user == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = "User account not found.";

                return response;
            }

            var account = new AccountResponse
            {
                Id = user.Id,
                Username = user.Username,
            };

            response.StatusCode = HttpStatusCode.OK;
            response.Result = account;

        }
        catch (Exception ex)
        {
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Message = string.IsNullOrWhiteSpace(ex.InnerException?.Message) ? ex.InnerException?.Message : ex.Message;
        }

        return await Task.FromResult(response);
    }

    public async Task<ClaimsIdentity?> AuthenticateUserAsync(LoginRequest loginRequest)
    {
        if (string.IsNullOrWhiteSpace(loginRequest.Username) || string.IsNullOrWhiteSpace(loginRequest.Password))
            return null;

        ClaimsIdentity? claimsIdentity = null;

        try
        {
            var user = _userRepository.GetUserByAuth(loginRequest.Username, loginRequest.Password);

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
