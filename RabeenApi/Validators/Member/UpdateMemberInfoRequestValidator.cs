using FluentValidation;
using RabeenApi.Dtos.Member.Requests;

namespace RabeenApi.Validators.Member;

public class UpdateMemberInfoRequestValidator:AbstractValidator<UpdateMemberInfoRequest>
{
    public UpdateMemberInfoRequestValidator()
    {
        RuleFor(member => member.Name)
            .NotEmpty();

        RuleFor(member => member.Title)
            .NotEmpty();

        RuleFor(member => member.About)
            .NotEmpty();
    }
}