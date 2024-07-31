using Microsoft.AspNetCore.Mvc;

namespace RabeenApi.Dtos.Association.Requests;

public record GetAllAssociationsRequest
{
    [FromQuery(Name = "page")] public int PageNumber { get; init; }

    [FromQuery(Name = "length")] public int PageLength { get; init; }

}