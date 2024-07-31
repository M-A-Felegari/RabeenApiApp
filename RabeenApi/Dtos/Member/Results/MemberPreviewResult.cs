using System.Text.Json.Serialization;

namespace RabeenApi.Dtos.Member.Results;

public record MemberPreviewResult(
    int Id,
    string Name,
    string Title,
    bool IsMainMember
    );