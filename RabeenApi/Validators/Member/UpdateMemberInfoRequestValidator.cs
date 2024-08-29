using FluentValidation;
using RabeenApi.Dtos.Member.Requests;

namespace RabeenApi.Validators.Member;

public class UpdateMemberInfoRequestValidator:AbstractValidator<UpdateMemberInfoRequest>
{
    public UpdateMemberInfoRequestValidator()
    {
        RuleFor(member => member.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(member => member.Title)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(member => member.About)
            .NotEmpty()
            .MaximumLength(450)
            .When(member => member.IsMainMember); //this property is only for main members

        RuleFor(member => member.OwnPortfolio)
            .MaximumLength(150)
            .When(member => member.IsMainMember);

    }
}