using FluentValidation;
using RabeenApi.Dtos.User.Requests;

namespace RabeenApi.Validators.User;

public class UserSignupRequestValidator:AbstractValidator<UserSignupRequest>
{
    public UserSignupRequestValidator()
    {
        RuleFor(s => s.Username)
            .Length(6, 20);

        RuleFor(s => s.Password)
            .NotEmpty()
            .Length(8, 16);

        RuleFor(s => s.Role)
            .IsInEnum();
    }
}