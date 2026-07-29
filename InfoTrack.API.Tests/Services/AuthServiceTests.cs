using InfoTrack.API.Interfaces.Repositories;
using InfoTrack.API.Models.Data;
using InfoTrack.API.Models.Requests;
using InfoTrack.API.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Moq;
using System.Net;
using System.Security.Claims;

namespace InfoTrack.API.Tests.Services;

[TestFixture]
public class AuthServiceTests
{
    private Mock<IUserRepository> _userRepositoryMock;

    private AuthService _authService;

    [SetUp]
    public void SetUp()
    {
        _userRepositoryMock = new Mock<IUserRepository>();

        _authService = new AuthService(_userRepositoryMock.Object);
    }

    [Test]
    [TestCase(null)]
    [TestCase(" ")]
    public async Task GetUserAccountAsync_InvalidRequests_ReturnsUnauthorized(string? id)
    {
        var result = await _authService.GetUserAccountAsync(null);

        Assert.Multiple(() =>
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            Assert.That(result.Message, Is.EqualTo("Unauthorized request."));
        });
    }

    [Test]
    public async Task GetUserAccountAsync_WhenUserExists_ReturnsOkWithAccount()
    {
        var user = new User { Id = 1001, Username = "jeremy.olu", Password = "Password123*" };

        _userRepositoryMock.Setup(x => x.GetUser(1001)).Returns(user);

        var result = await _authService.GetUserAccountAsync("1001");

        Assert.Multiple(() =>
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Result?.Id, Is.EqualTo(user.Id));
            Assert.That(result.Result?.Username, Is.EqualTo(user.Username));
        });
    }

    [Test]
    public async Task GetUserAccountAsync_WhenUserNotFound_ReturnsNotFound()
    {
        _userRepositoryMock.Setup(x => x.GetUser(It.IsAny<int>())).Returns(() => null);

        var result = await _authService.GetUserAccountAsync("1005");

        Assert.Multiple(() =>
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(result.Message, Is.EqualTo("User account not found."));
        });
    }

    [Test]
    public async Task GetUserAccountAsync_WhenRepositoryThrows_ReturnsInternalServerError()
    {
        _userRepositoryMock.Setup(x => x.GetUser(It.IsAny<int>()))
            .Throws(new Exception("Data source error"));

        var result = await _authService.GetUserAccountAsync("1001");

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
    }

    [Test]
    public async Task GetUserAccountAsync_WhenIdIsNonNumeric_ReturnsInternalServerError()
    {
        _userRepositoryMock.Setup(x => x.GetUser(0)).Returns(() => null);

        var result = await _authService.GetUserAccountAsync("abc");

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));

        _userRepositoryMock.Verify(x => x.GetUser(0), Times.Never);
    }

    [Test]
    public async Task AuthenticateUserAsync_WhenUsernameIsEmpty_ReturnsNull()
    {
        var request = new LoginRequest { Username = "", Password = "password" };

        var result = await _authService.AuthenticateUserAsync(request);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task AuthenticateUserAsync_WhenPasswordIsEmpty_ReturnsNull()
    {
        var request = new LoginRequest { Username = "jeremy.olu", Password = "" };

        var result = await _authService.AuthenticateUserAsync(request);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task AuthenticateUserAsync_WhenCredentialsInvalid_ReturnsNull()
    {
        _userRepositoryMock
            .Setup(x => x.GetUserByAuth(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(() => null);

        var request = new LoginRequest { Username = "jeremy.olu", Password = "wrongpass" };

        var result = await _authService.AuthenticateUserAsync(request);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task AuthenticateUserAsync_WhenCredentialsValid_ReturnsClaimsIdentityWithCorrectClaims()
    {
        var user = new User { Id = 1001, Username = "jeremy.olu", Password = "Password123*" };

        _userRepositoryMock
            .Setup(x => x.GetUserByAuth("jeremy.olu", "Password123*"))
            .Returns(user);

        var request = new LoginRequest { Username = "jeremy.olu", Password = "Password123*" };

        var result = await _authService.AuthenticateUserAsync(request);

        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result!.AuthenticationType, Is.EqualTo(CookieAuthenticationDefaults.AuthenticationScheme));
            Assert.That(result.FindFirst(ClaimTypes.NameIdentifier)?.Value, Is.EqualTo(user.Id.ToString()));
            Assert.That(result.FindFirst(ClaimTypes.Name)?.Value, Is.EqualTo(user.Username));
        });
    }

    [Test]
    public async Task AuthenticateUserAsync_WhenRepositoryThrows_ReturnsNull()
    {
        _userRepositoryMock
            .Setup(x => x.GetUserByAuth(It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception("Data source error"));

        var request = new LoginRequest { Username = "jeremy.olu", Password = "Password123*" };

        var result = await _authService.AuthenticateUserAsync(request);

        Assert.That(result, Is.Null);
    }
}