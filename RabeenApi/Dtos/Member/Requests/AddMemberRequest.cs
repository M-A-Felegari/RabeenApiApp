using System.Text.Json.Serialization;
using RabeenApi.Dtos.Achievement.Requests;

namespace RabeenApi.Dtos.Member.Requests;

public record AddMemberRequest(
    string Name,
    string Title,
    string About,
    bool IsMainMember
);