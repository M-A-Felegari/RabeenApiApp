using FluentValidation;
using RabeenApi.Dtos.Member.Requests;

namespace RabeenApi.Validators.Member;

public class UpdateMemberRequestInfoValidator:AbstractValidator<UpdateMemberInfoRequest>
{
    public UpdateMemberRequestInfoValidator()
    {
        RuleFor(member => member.Name)
            .NotEmpty();

        RuleFor(member => member.Title)
            .NotEmpty();

        RuleFor(member => member.About)
            .NotEmpty();
    }
}