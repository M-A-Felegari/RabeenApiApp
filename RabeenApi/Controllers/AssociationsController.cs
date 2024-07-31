using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos;
using RabeenApi.Dtos.Association.Requests;
using RabeenApi.Dtos.Association.Results;
using RabeenApi.Dtos.AssociationCooperation.Requests;
using RabeenApi.Dtos.AssociationCooperation.Results;
using RabeenApi.Factories;
using RabeenApi.Services.Implementations;

namespace RabeenApi.Controllers;

[ApiController]
[Route("associations")]
public class AssociationsController(
    AssociationService associationService,
    AssociationCooperationService cooperationService,
    ActionResultHandlersFactory handlersFactory) : BaseControllerClass(handlersFactory)
{
    private readonly AssociationService _associationService = associationService;
    private readonly AssociationCooperationService _cooperationService = cooperationService;

    [HttpGet()]
    public async Task<ActionResult<BaseResult<PaginatedResult<AssociationInfoResult>>>> GetAllAsync(
        [FromQuery] GetAllAssociationsRequest request)
    {
        var result = await _associationService.GetAllAsync(request);

        return GetActionResultToReturn(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BaseResult<AssociationInfoResult>>> GetAssociationInfoAsync(int id)
    {
        var result = await _associationService.GetAssociationInfoAsync(id);

        return GetActionResultToReturn(result);
    }

    [HttpPost()]
    public async Task<ActionResult<BaseResult<AssociationInfoResult>>> AddAssociationAsync(
        AddAssociationRequest request)
    {
        var result = await _associationService.AddAssociationAsync(request);

        return GetActionResultToReturn(result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<BaseResult<AssociationInfoResult>>> UpdateAssociationInfoAsync(int id,
        UpdateAssociationRequest request)
    {
        var result = await _associationService.UpdateAssociationInfoAsync(id, request);

        return GetActionResultToReturn(result);
    }

    [HttpPost("{id:int}/set-logo")]
    public async Task<ActionResult<BaseResult<object>>> SetAssociationLogoAsync(int id,
        [FromForm] SetAssociationLogoRequest request)
    {
        var result = await _associationService.SetAssociationLogoAsync(id, request);

        return GetActionResultToReturn(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<BaseResult<object>>> DeleteAssociationAsync(int id)
    {
        var result = await _associationService.DeleteAssociationAsync(id);

        return GetActionResultToReturn(result);
    }

    [HttpGet("{id:int}/cooperations")]
    public async Task<ActionResult<BaseResult<PaginatedResult<AssociationCooperationResult>>>> GetAllCooperationsAsync(
        int id, [FromQuery] GetAllAssociationCooperationsRequest request)
    {
        var result = await _cooperationService.GetAllCooperationsAsync(id,request);

        return GetActionResultToReturn(result);
    }

    [HttpPost("{id:int}/cooperations")]
    public async Task<ActionResult<BaseResult<AssociationCooperationResult>>> AddCooperationAsync( int id,
        [FromForm] AddCooperationRequest request)
    {
        var result = await _cooperationService.AddCooperationAsync(id,request);

        return GetActionResultToReturn(result);
    }
}