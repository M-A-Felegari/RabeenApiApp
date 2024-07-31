using Microsoft.AspNetCore.Mvc;

namespace RabeenApi.Dtos.AssociationCooperation.Requests;

public record GetAllAssociationCooperationsRequest
{
    [FromQuery(Name = "page")] public int PageNumber { get; init; }

    [FromQuery(Name = "length")] public int PageLength { get; init; }

}