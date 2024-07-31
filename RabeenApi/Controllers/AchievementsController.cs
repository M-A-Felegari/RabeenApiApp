using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos;
using RabeenApi.Dtos.Achievement.Requests;
using RabeenApi.Dtos.Achievement.Results;
using RabeenApi.Factories;
using RabeenApi.Services.Implementations;

namespace RabeenApi.Controllers;

[ApiController]
[Route("achievements")]
public class AchievementsController(AchievementsService achievementsService,
    ActionResultHandlersFactory handlersFactory) : BaseControllerClass(handlersFactory)
{
    private readonly AchievementsService _achievementsService = achievementsService;

    [HttpPut("{id:int}")]
    public async Task<ActionResult<BaseResult<AchievementResult>>> UpdateAchievementAsync(int id,
        UpdateAchievementRequest request)
    {
        var result = await _achievementsService.UpdateAchievementAsync(id,request);

        return GetActionResultToReturn(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<BaseResult<object>>> DeleteAchievementAsync(int id)
    {
        var result = await _achievementsService.DeleteAchievementAsync(id);

        return GetActionResultToReturn(result);
    }
}
