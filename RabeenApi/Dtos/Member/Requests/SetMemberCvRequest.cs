namespace RabeenApi.Dtos.Member.Requests;

public record SetMemberCvRequest(int Id, IFormFile CvFile);