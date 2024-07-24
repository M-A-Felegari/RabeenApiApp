namespace RabeenApi.Dtos.Results;

public record AssociationInfoResult(
    int Id,
    string Name,
    string UniversityName,
    string ContactLink,
    DateTime CreationDate,
    DateTime FirstCooperationDate
);