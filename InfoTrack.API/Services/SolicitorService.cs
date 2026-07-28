using InfoTrack.API.Extentions;
using InfoTrack.API.Interfaces.Providers;
using InfoTrack.API.Interfaces.Services;
using InfoTrack.API.Models.Responses;
using System.Net;
using System.Text.RegularExpressions;

namespace InfoTrack.API.Services;

public class SolicitorService : ISolicitorService
{
    private readonly ISolicitorProvider _solicitorProvider;

    public SolicitorService(ISolicitorProvider solicitorProvider)
    {
        _solicitorProvider = solicitorProvider;
    }

    public async Task<ResultsResponse<SolicitorResponse>> GetSolicitorsByLocationAsync(string location, string? sortBy)
    {
        var response = new ResultsResponse<SolicitorResponse>();

        var solicitors = new List<SolicitorResponse>();

        try
        {

            var results = await _solicitorProvider.GetSolicitorsByLocationAsync(location.ToLower());

            if (results == null || !results.Any())
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = $"Unable to retrive {location} solicitors at the moment.";

                return response;
            }

            results?.ToList().ForEach(x =>
            {
                var solicitor = new SolicitorResponse
                {
                    Name = RemoveHtml(x.Name),
                    Address = FormatAddress(x.Address, location),
                    Description = x.Description,
                    ContactDetails = new Contact
                    {
                        Telephone = x.Telephone,
                        Website = x.Website
                    },
                    LogoUrl = $"https://www.solicitors.com/{x.LogoUrl}",
                };

                solicitors.Add(solicitor);
            });

            response.StatusCode = HttpStatusCode.OK;
            response.Count = solicitors.Count();
            response.Results = Sort(sortBy, solicitors);
        }
        catch (Exception ex)
        {
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Message = !string.IsNullOrWhiteSpace(ex.InnerException?.Message) ? ex.InnerException.Message : ex.Message; 
        }

        return response;
    }

    private List<SolicitorResponse> Sort(string? sortBy, IEnumerable<SolicitorResponse> solicitors)
    {
        switch (sortBy?.ToLower())
        {
            case "name_asc":
                return solicitors.OrderBy(x => x.Name).ToList();

            case "name_desc":
                return solicitors.OrderByDescending(x => x.Name).ToList();

            default:
                return solicitors.ToList();
        }
    }

    private string? RemoveHtml(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;
        
        return Regex.Replace(input, "<[^>]*>", string.Empty).Trim();
    }

    private Address FormatAddress(string? address, string location)
    {
        if (string.IsNullOrWhiteSpace(address))
            return new Address();

        var parts = address.Split(',', StringSplitOptions.TrimEntries);

        var formattedAddress = new Address
        {
            AddressLine1 = parts[0],
            Location = location.Capitalize(),
            Postcode = GetPostcode(address)
        };

        return formattedAddress;
    }

    private string GetPostcode(string address)
    {
        var match = Regex.Match(address, @"[A-Z]{1,2}\d[A-Z\d]?\s*\d[A-Z]{2}", RegexOptions.IgnoreCase);

        return match.Success ? match.Value : string.Empty;
    }
}
