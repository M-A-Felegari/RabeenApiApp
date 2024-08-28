using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos;
using RabeenApi.Dtos.AssociationCooperation.Requests;
using RabeenApi.Dtos.AssociationCooperation.Results;
using RabeenApi.Factories;
using RabeenApi.Services;
using RabeenApi.Services.Implementations;

namespace RabeenApi.Controllers;

[ApiController]
[Route("cooperations")]
public class AssociationCooperationsController(IAssociationCooperationService cooperationService,
    ActionResultHandlersFactory handlersFactory) : BaseControllerClass(handlersFactory)
{
    private readonly IAssociationCooperationService _cooperationService = cooperationService;
    

    [HttpPut("{id:int}")]
    [Authorize("ManagerOrAdminPolicy")]
    public async Task<ActionResult<BaseResult<AssociationCooperationResult>>> UpdateCooperationAsync(int id,
        [FromForm] UpdateCooperationRequest request)
    {
        var result = await _cooperationService.UpdateCooperationAsync(id,request);

        return GetActionResultToReturn(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize("ManagerOrAdminPolicy")]
    public async Task<ActionResult<BaseResult<object>>> DeleteCooperationAsync(int id)
    {
        var result = await _cooperationService.DeleteCooperationAsync(id);

        return GetActionResultToReturn(result);
    }
}
