using System.Text.Json.Serialization;
using RabeenApi.Dtos.Achievement.Results;

namespace RabeenApi.Dtos.Member.Results;

public record MemberInfoResult(
    int Id,
    string Name,
    string Title,
    string About,
     bool IsMainMember,
    string OwnPortfolio
    );