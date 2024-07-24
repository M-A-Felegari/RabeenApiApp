namespace RabeenApi.Dtos.AssociationCooperation.Requests;

public record AddCooperationRequest(
    int AssociationId,
    string Title,
    string Description,
    DateTime StartDate,
    DateTime FinishDate,
    IFormFile Image
);