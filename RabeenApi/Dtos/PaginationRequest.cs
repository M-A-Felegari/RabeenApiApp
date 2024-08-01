using Microsoft.AspNetCore.Mvc;

namespace RabeenApi.Dtos;

public record PaginationRequest
{
    [FromQuery(Name = "page")] public int PageNumber { get; init; }

    [FromQuery(Name = "length")] public int PageLength { get; init; }

}