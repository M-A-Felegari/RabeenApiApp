using DataAccess.Models;

namespace RabeenApi.Dtos.User.Requests;

public record UserSignupRequest(string Username, string Password, UserRole Role);