using System.Text.Json.Serialization;

namespace RabeenApi.Dtos.Member.Requests;

public record UpdateMemberInfoRequest(
    string Name,
    string Title,
    bool IsMainMember,
    string About
    );