namespace RabeenApi.Dtos.Association.Requests;

public record UpdateAssociationRequest(
    string Name,
    string UniversityName,
    string ContactLink,
    DateTime CreationDate
);