using Microsoft.AspNetCore.Mvc;

namespace RabeenApi.Dtos.ContactMessage.Requests;

public record GetAllContactMessagesRequest
{
    [FromQuery(Name = "page")] public int PageNumber { get; init; }

    [FromQuery(Name = "length")] public int PageLength { get; init; }

}