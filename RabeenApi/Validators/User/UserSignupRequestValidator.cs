using FluentValidation;
using RabeenApi.Dtos.User.Requests;

namespace RabeenApi.Validators.User;

public class UserSignupRequestValidator:AbstractValidator<UserSignupRequest>
{
    public UserSignupRequestValidator()
    {
        RuleFor(s => s.Username)
            .Length(4, 20);

        RuleFor(s => s.Password)
            .Length(8, 30);

        RuleFor(s => s.Role)
            .IsInEnum();
    }
}