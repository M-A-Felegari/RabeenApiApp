using FluentValidation;
using RabeenApi.Dtos.Member.Requests;

namespace RabeenApi.Validators.Member;

public class SetProfilePictureRequestValidator:AbstractValidator<SetProfilePictureRequest>
{
    public SetProfilePictureRequestValidator()
    {
        RuleFor(member => member.Picture)
            .NotNull();
    }
}