namespace RabeenApi.Dtos.Requests;

public record UpdateCooperationRequest(
    int Id,
    string Title,
    string Description,
    DateTime StartDate,
    DateTime FinishDate,
    IFormFile Image
);