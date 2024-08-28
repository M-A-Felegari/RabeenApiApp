namespace RabeenApi.Dtos.Member.Requests;

public record AddMemberRequest(
    string Name,
    string Title,
    string About,
    bool IsMainMember,
    string OwnPortfolio
);