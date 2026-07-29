using InfoTrack.API.Interfaces.Providers;
using InfoTrack.API.Models.Data;
using InfoTrack.API.Services;
using Moq;
using System.Net;

namespace InfoTrack.API.Tests.Services;

[TestFixture]
public class SolicitorServiceTests
{
    private Mock<ISolicitorProvider> _solicitorProviderMock;

    private SolicitorService _solicitorService;

    [SetUp]
    public void SetUp()
    {
        _solicitorProviderMock = new Mock<ISolicitorProvider>();

        _solicitorService = new SolicitorService(_solicitorProviderMock.Object);
    }

    [Test]
    public async Task GetSolicitorsByLocationAsync_WhenProviderReturnsNull_ReturnsNotFound()
    {
        _solicitorProviderMock.Setup(x => x.GetSolicitorsByLocationAsync("london"))
            .ReturnsAsync(new List<Solicitor>());

        var result = await _solicitorService.GetSolicitorsByLocationAsync("london", null);

        Assert.Multiple(() =>
        {
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(result.Message, Is.EqualTo("Unable to retrive london solicitors at the moment."));
        });
    }

    [Test]
    public async Task GetSolicitorsByLocationAsync_WhenProviderReturnsEmptyList_ReturnsNotFound()
    {
        _solicitorProviderMock.Setup(X => X.GetSolicitorsByLocationAsync("london"))
            .ReturnsAsync(new List<Solicitor>());

        var result = await _solicitorService.GetSolicitorsByLocationAsync("london", null);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetSolicitorsByLocationAsync_WhenProviderReturnsResults_ResultsAreNotEmpty()
    {
        var results = new List<Solicitor>
        {
            new Solicitor
            {
                Name = "Smith & Co",
                Address = "1 High Street, London, SW1A 1AA",
                Description = "A test local firm",
                Telephone = "020 1234 5678",
                Website = "https://smithandco.com",
                LogoUrl = "logo.png"
            }
        };

        _solicitorProviderMock.Setup(x => x.GetSolicitorsByLocationAsync("london")).ReturnsAsync(results);

        var result = await _solicitorService.GetSolicitorsByLocationAsync("london", null);

        Assert.That(result.Results, Is.Not.Empty);
        Assert.That(result.Results, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count, Is.EqualTo(results.Count()));
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        });
    }
}