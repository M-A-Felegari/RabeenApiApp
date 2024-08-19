using FluentValidation;
using RabeenApi.Dtos.Member.Requests;

namespace RabeenApi.Validators.Member;

public class UpdateMemberInfoRequestValidator:AbstractValidator<UpdateMemberInfoRequest>
{
    public UpdateMemberInfoRequestValidator()
    {
        RuleFor(member => member.Name)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(member => member.Title)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(member => member.About)
            .MaximumLength(1000);
    }
}