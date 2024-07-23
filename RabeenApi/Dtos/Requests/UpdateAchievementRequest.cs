namespace RabeenApi.Dtos.Requests;

public record UpdateAchievementRequest(int Id, string Title, string Description, DateTime Date);