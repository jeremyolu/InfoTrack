using InfoTrack.API.Interfaces.Providers;
using InfoTrack.API.Models.Data;
using System.Text.RegularExpressions;

namespace InfoTrack.API.Providers;

public class SolicitorScraperProvider : ISolicitorProvider
{
    private readonly HttpClient _httpClient;

    public SolicitorScraperProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Solicitor>> GetSolicitorsByLocationAsync(string location)
    {
        var solicitors = new List<Solicitor>();

        var url = $"{location}-solicitors.html";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Http response failed");

        var html = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(html))
            throw new Exception("Html content is empty");

        var sections = html.Split(@"<div class=""result-item"">", StringSplitOptions.RemoveEmptyEntries);

        foreach (var section in sections)
        {
            var silicitor = new Solicitor();

            silicitor.Name = GetValueFromHtmlElement(section, @"<span class=""h2"">(.*?)<div");
            silicitor.Address = GetValueFromHtmlElement(section, @"<address>(.*?)</address>");
            silicitor.Description = GetValueFromHtmlElement(section, @"<p>(.*?)</p>");
            silicitor.Telephone = GetValueFromHtmlElement(section, @"href=""tel:([^""]+)""");
            silicitor.Website = GetValueFromHtmlElement(section, @"target=""_blank"" href=""(.*?)""");
            silicitor.LogoUrl = GetValueFromHtmlElement(section, @"<img[^>]*src=""([^""]+)""");

            if (!string.IsNullOrEmpty(silicitor.Name))
            {
                solicitors.Add(silicitor);
            }
        }

        return solicitors;
    }

    private string? GetValueFromHtmlElement(string input, string htmlElementPattern)
    {
        return Regex.Match(input, htmlElementPattern, RegexOptions.Singleline)
            .Groups[1].Value.Trim();
    }
}
