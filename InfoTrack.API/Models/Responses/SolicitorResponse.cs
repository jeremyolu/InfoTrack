namespace InfoTrack.API.Models.Responses;

public class SolicitorResponse
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public required Address Address { get; set; }
    public required Contact ContactDetails { get; set; }
    public string? LogoUrl { get; set; }
}

public class Address
{
    public string? AddressLine1 { get; set; }
    public string? Location { get; set; }
    public string? Postcode { get; set; }
}

public class Contact
{
    public string? Telephone { get; set; }
    public string? Website { get; set; }
}