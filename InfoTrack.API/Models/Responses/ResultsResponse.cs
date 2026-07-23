using System.Net;

namespace InfoTrack.API.Models.Responses;

public class ResultsResponse<T> : BaseResponse
{
    public int Count { get; set; }
    public IEnumerable<T> Results { get; set; } = [];
}