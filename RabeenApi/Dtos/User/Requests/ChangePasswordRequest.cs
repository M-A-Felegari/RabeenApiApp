namespace RabeenApi.Dtos.User.Requests;

public record ChangePasswordRequest(string OldPassword,string NewPassword);