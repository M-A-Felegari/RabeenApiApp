using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos;
using RabeenApi.Dtos.AssociationCooperation.Requests;
using RabeenApi.Dtos.AssociationCooperation.Results;
using RabeenApi.Factories;
using RabeenApi.Services.Implementations;

namespace RabeenApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class AssociationCooperationController(AssociationCooperationService cooperationService,
    ActionResultHandlersFactory handlersFactory) : BaseControllerClass(handlersFactory)
{
    private readonly AssociationCooperationService _cooperationService = cooperationService;

    [HttpGet("all-cooperations")]
    public async Task<ActionResult<BaseResult<PaginatedResult<AssociationCooperationResult>>>> GetAllCooperationsAsync(
        [FromQuery] GetAllAssociationCooperationsRequest request)
    {
        var result = await _cooperationService.GetAllCooperationsAsync(request);

        return GetActionResultToReturn(result);
    }

    [HttpPost("add-cooperation")]
    public async Task<ActionResult<BaseResult<AssociationCooperationResult>>> AddCooperationAsync([FromForm] AddCooperationRequest request)
    {
        var result = await _cooperationService.AddCooperationAsync(request);

        return GetActionResultToReturn(result);
    }

    [HttpPut("update-cooperation")]
    public async Task<ActionResult<BaseResult<AssociationCooperationResult>>> UpdateCooperationAsync([FromForm] UpdateCooperationRequest request)
    {
        var result = await _cooperationService.UpdateCooperationAsync(request);

        return GetActionResultToReturn(result);
    }

    [HttpDelete("delete-cooperation")]
    public async Task<ActionResult<BaseResult<object>>> DeleteCooperationAsync([FromQuery] DeleteCooperationRequest request)
    {
        var result = await _cooperationService.DeleteCooperationAsync(request);

        return GetActionResultToReturn(result);
    }
}
