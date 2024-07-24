namespace RabeenApi.Dtos.Member.Requests;

public record UpdateMemberInfoRequest(
    int Id,
    string Name,
    string Title,
    bool IsMainMember,
    string About
    );