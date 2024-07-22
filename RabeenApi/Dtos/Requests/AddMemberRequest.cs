namespace RabeenApi.Dtos.Requests;

public record AddMemberRequest(string Name,
    string Title,
    string About,
    bool IsMainMember,
    List<AddAchievementRequest> Achievements
    );