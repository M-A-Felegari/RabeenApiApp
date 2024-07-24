using Microsoft.AspNetCore.Mvc;
using RabeenApi.Services.Implementations;

namespace RabeenApi.Controllers;

public class AchievementController(AchievementService achievementService) : ControllerBase
{
    private readonly AchievementService _achievementService = achievementService;


}