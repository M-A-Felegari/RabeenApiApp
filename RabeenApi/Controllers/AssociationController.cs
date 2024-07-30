using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos;
using RabeenApi.Dtos.Association.Requests;
using RabeenApi.Dtos.Association.Results;
using RabeenApi.Factories;
using RabeenApi.Services.Implementations;

namespace RabeenApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class AssociationController(AssociationService associationService,
    ActionResultHandlersFactory handlersFactory) : BaseControllerClass(handlersFactory)
{
    private readonly AssociationService _associationService = associationService;

    [HttpGet("all")]
    public async Task<ActionResult<BaseResult<PaginatedResult<AssociationInfoResult>>>> GetAllAsync(
        [FromQuery]GetAllAssociationsRequest request)
    {
        var result = await _associationService.GetAllAsync(request);

        return GetActionResultToReturn(result);
    }

    [HttpGet("info")]
    public async Task<ActionResult<BaseResult<AssociationInfoResult>>> GetAssociationInfoAsync(
        [FromQuery]GetAssociationRequest request)
    {
        var result = await _associationService.GetAssociationInfoAsync(request);

        return GetActionResultToReturn(result);
    }

    [HttpPost("add")]
    public async Task<ActionResult<BaseResult<AssociationInfoResult>>> AddAssociationAsync(AddAssociationRequest request)
    {
        var result = await _associationService.AddAssociationAsync(request);

        return GetActionResultToReturn(result);
    }

    [HttpPut("update")]
    public async Task<ActionResult<BaseResult<AssociationInfoResult>>> UpdateAssociationInfoAsync(UpdateAssociationRequest request)
    {
        var result = await _associationService.UpdateAssociationInfoAsync(request);

        return GetActionResultToReturn(result);
    }

    [HttpPost("set-logo")]
    public async Task<ActionResult<BaseResult<object>>> SetAssociationLogoAsync([FromForm] SetAssociationLogoRequest request)
    {
        var result = await _associationService.SetAssociationLogoAsync(request);

        return GetActionResultToReturn(result);
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<BaseResult<object>>> DeleteAssociationAsync([FromQuery] DeleteAssociationRequest request)
    {
        var result = await _associationService.DeleteAssociationAsync(request);

        return GetActionResultToReturn(result);
    }
}
