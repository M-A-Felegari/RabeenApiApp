namespace RabeenApi.Dtos.Requests;

public record AddAssociationRequest(
    string Name,
    string UniversityName,
    string ContactLink,
    DateTime CreationDate,
    DateTime FirstCooperationDate
);