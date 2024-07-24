namespace RabeenApi.Dtos.Member.Requests;

public record SetProfilePictureRequest(int Id, IFormFile Picture);