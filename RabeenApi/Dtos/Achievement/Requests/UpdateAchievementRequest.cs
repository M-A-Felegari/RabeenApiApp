using Microsoft.AspNetCore.Mvc;

namespace RabeenApi.Dtos.Achievement.Requests;

public record UpdateAchievementRequest(string Title, string Description, DateTime Date);