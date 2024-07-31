using System.Text.Json.Serialization;

namespace RabeenApi.Dtos.Member.Requests;

public record SetMemberCvRequest(IFormFile CvFile);