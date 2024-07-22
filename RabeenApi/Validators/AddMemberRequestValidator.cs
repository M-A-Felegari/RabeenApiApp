using FluentValidation;
using RabeenApi.Dtos.Requests;

namespace RabeenApi.Validators;

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
            .NotEmpty()
            .MaximumLength(200)
            .When(member => member.IsMainMember); //this property is only for main members

        RuleForEach(member => member.Achievements)
            .SetValidator(new AddAchievementRequestValidator())
            .When(member=>member.IsMainMember); //only main members have achievements
    }
}