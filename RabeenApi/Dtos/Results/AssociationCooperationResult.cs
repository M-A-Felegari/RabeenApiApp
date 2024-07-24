namespace RabeenApi.Dtos.Results;

public record AssociationCooperationResult(
    int Id,
    string Title,
    string Description,
    DateTime StartDate,
    DateTime FinishDate
);