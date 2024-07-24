namespace RabeenApi.Dtos.Achievement.Requests;

public record AddAchievementToExistMemberRequest(int MemberId, string Title, string Description, DateTime Date);