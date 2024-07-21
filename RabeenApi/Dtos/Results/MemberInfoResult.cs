using DataAccess.Models;

namespace RabeenApi.Dtos.Results;

public record MemberInfoResult(
    int Id,
    string Name,
    string Title,
    string About,
    bool IsMain,
    List<Achievement> Achievments
    );