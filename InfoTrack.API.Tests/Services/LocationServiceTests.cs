using InfoTrack.API.Interfaces.Repositories;
using InfoTrack.API.Models.Data;
using InfoTrack.API.Services;
using Moq;
using System.Net;

namespace InfoTrack.API.Tests.Services;

[TestFixture]
public class LocationServiceTests
{
    private Mock<ILocationRepository> _locationRepositoryMock;

    private LocationService _locationService;

    [SetUp]
    public void SetUp()
    {
        _locationRepositoryMock = new Mock<ILocationRepository>();

        _locationService = new LocationService(_locationRepositoryMock.Object);
    }

    [Test]
    public async Task GetLocations_WhenRepositoryReturnsData_ReturnsOkWithResults()
    {
        // Arrange
        var locations = new List<Location>
        {
            new Location { Name = "Lodnon" },
            new Location { Name = "Manchester" }
        };

        _locationRepositoryMock.Setup(repo => repo.GetLocations()).Returns(locations);

        // Act
        var result = await _locationService.GetLocations();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Count, Is.EqualTo(locations.Count()));
            Assert.That(result.Results, Is.EqualTo(locations));
            Assert.That(result.Message, Is.Null.Or.Empty);
        });
    }

    [Test]
    public async Task GetLocations_WhenRepositoryReturnsEmptyList_ReturnsOkWithZeroCount()
    {
        // Arrange
        var locations = new List<Location>();

        _locationRepositoryMock.Setup(repo => repo.GetLocations()).Returns(locations);

        // Act
        var result = await _locationService.GetLocations();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(result.Count, Is.EqualTo(locations.Count()));
            Assert.That(result.Results, Is.Empty);
        });
    }

    [Test]
    public async Task GetLocations_WhenRepositoryThrowsException_ReturnsInternalServerErrorWithMessage()
    {
        // Arrange
        var exceptionMessage = "Data connection failed";

        _locationRepositoryMock.Setup(repo => repo.GetLocations()).Throws(new Exception(exceptionMessage));

        // Act
        var result = await _locationService.GetLocations();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
            Assert.That(result.Message, Is.EqualTo(exceptionMessage));
        });
    }
}