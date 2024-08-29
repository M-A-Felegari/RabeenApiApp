using FluentValidation;
using RabeenApi.Dtos.User.Requests;

namespace RabeenApi.Validators.User;

public class UserSignupRequestValidator:AbstractValidator<UserSignupRequest>
{
    public UserSignupRequestValidator()
    {
        RuleFor(s => s.Username)
            .Length(4, 32);

        RuleFor(s => s.Password)
            .Length(8, 32);

        RuleFor(s => s.Role)
            .IsInEnum();
    }
}