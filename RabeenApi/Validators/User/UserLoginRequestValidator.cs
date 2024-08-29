using FluentValidation;
using RabeenApi.Dtos.User.Requests;

namespace RabeenApi.Validators.User;

public class UserLoginRequestValidator:AbstractValidator<UserLoginRequest>
{
    public UserLoginRequestValidator()
    {
        RuleFor(l => l.Username)
            .Length(4,32);

        RuleFor(l => l.Password)
            .Length(8, 32);
    }
}