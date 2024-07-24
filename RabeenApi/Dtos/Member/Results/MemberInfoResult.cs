using RabeenApi.Dtos.Achievement.Results;

namespace RabeenApi.Dtos.Member.Results;

public record MemberInfoResult(
    int Id,
    string Name,
    string Title,
    string About,
    bool IsMain,
    List<AchievementResult> Achievments
    );