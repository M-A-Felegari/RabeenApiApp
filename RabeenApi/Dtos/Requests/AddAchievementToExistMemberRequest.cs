namespace RabeenApi.Dtos.Requests;

public record AddAchievementToExistMemberRequest(int MemberId, string Title, string Description, DateTime Date);