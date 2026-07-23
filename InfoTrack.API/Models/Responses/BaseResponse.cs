using System.Net;

namespace InfoTrack.API.Models.Responses;

public class BaseResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public string? Message { get; set; }
}