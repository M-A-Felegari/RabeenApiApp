using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos;
using RabeenApi.Dtos.Achievement.Requests;
using RabeenApi.Dtos.Achievement.Results;
using RabeenApi.Factories;
using RabeenApi.Services.Implementations;

namespace RabeenApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class AchievementController(AchievementService achievementService,
    ActionResultHandlersFactory handlersFactory) : BaseControllerClass(handlersFactory)
{
    private readonly AchievementService _achievementService = achievementService;

    [HttpPost("update")]
    public async Task<ActionResult<BaseResult<AchievementResult>>> UpdateAchievementAsync(UpdateAchievementRequest request)
    {
        var result = await _achievementService.UpdateAchievementAsync(request);

        return GetActionResultToReturn(result);
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<BaseResult<object>>> DeleteAchievementAsync(DeleteAchievementRequest request)
    {
        var result = await _achievementService.DeleteAchievementAsync(request);

        return GetActionResultToReturn(result);
    }
}
