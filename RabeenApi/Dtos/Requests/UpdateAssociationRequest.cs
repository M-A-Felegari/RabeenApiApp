namespace RabeenApi.Dtos.Requests;

public record UpdateAssociationRequest(
    int Id,
    string Name,
    string UniversityName,
    string ContactLink,
    DateTime CreationDate,
    DateTime FirstCooperationDate
);