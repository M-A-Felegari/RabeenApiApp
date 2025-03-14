﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabeenApi.Dtos;
using RabeenApi.Dtos.Achievement.Requests;
using RabeenApi.Dtos.Achievement.Results;
using RabeenApi.Factories;
using RabeenApi.Services;
using RabeenApi.Services.Implementations;

namespace RabeenApi.Controllers;

[ApiController]
[Route("achievements")]
public class AchievementsController(IAchievementsService achievementsService,
    ActionResultHandlersFactory handlersFactory) : BaseControllerClass(handlersFactory)
{
    private readonly IAchievementsService _achievementsService = achievementsService;

    [HttpPut("{id:int}")]
    [Authorize("ManagerPolicy")]
    public async Task<ActionResult<BaseResult<AchievementResult>>> UpdateAchievementAsync(int id,
        UpdateAchievementRequest request)
    {
        var result = await _achievementsService.UpdateAchievementAsync(id,request);

        return GetActionResultToReturn(result);
    }

    [HttpDelete("{id:int}")]
    [Authorize("ManagerPolicy")]
    public async Task<ActionResult<BaseResult<object>>> DeleteAchievementAsync(int id)
    {
        var result = await _achievementsService.DeleteAchievementAsync(id);

        return GetActionResultToReturn(result);
    }
}
