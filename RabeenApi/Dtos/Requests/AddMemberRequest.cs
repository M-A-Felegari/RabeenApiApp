namespace RabeenApi.Dtos.Requests;

public record AddMemberRequest(string Name,
    string Title,
    string About,
    bool IsMain,
    List<AddAchievementRequest> Achievements
    );