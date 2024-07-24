using Microsoft.AspNetCore.Mvc;

namespace RabeenApi.Dtos.Requests;

public record SetProfilePictureRequest(int Id, IFormFile Picture);