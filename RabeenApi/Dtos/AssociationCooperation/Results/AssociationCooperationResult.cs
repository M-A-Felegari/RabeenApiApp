namespace RabeenApi.Dtos.AssociationCooperation.Results;

public record AssociationCooperationResult(
    int Id,
    string Title,
    string Description,
    DateTime StartDate,
    DateTime FinishDate
);