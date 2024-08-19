using FluentValidation;
using RabeenApi.Dtos.Member.Requests;
using RabeenApi.Validators.Achievement;

namespace RabeenApi.Validators.Member;

public class AddMemberRequestValidator : AbstractValidator<AddMemberRequest>
{
    public AddMemberRequestValidator()
    {
        RuleFor(member => member.Name)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(member => member.Title)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(member => member.About)
            .MaximumLength(1000)
            .When(member => member.IsMainMember); //this property is only for main members
        
    }
}