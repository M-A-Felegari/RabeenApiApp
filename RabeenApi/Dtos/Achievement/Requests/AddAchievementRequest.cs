namespace RabeenApi.Dtos.Achievement.Requests;

public record AddAchievementRequest(int MemberId, string Title, string Description, DateTime Date);