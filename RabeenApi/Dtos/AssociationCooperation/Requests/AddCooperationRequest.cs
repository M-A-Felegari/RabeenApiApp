namespace RabeenApi.Dtos.AssociationCooperation.Requests;

public record AddCooperationRequest(
    string Title,
    string Description,
    DateTime StartDate,
    DateTime FinishDate,
    IFormFile Image
);