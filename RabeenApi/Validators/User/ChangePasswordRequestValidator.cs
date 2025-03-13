using FluentValidation;
using RabeenApi.Dtos.User.Requests;

namespace RabeenApi.Validators.User;

public class ChangePasswordRequestValidator:AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(c => c.OldPassword)
            .Length(8, 32);
        
        RuleFor(c => c.NewPassword)
            .Length(8, 32);
    }
}