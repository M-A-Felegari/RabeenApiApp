using FluentValidation;
using RabeenApi.Dtos.Member.Requests;

namespace RabeenApi.Validators.Member;

public class SetMemberCvRequestValidator : AbstractValidator<SetMemberCvRequest>
{
    public SetMemberCvRequestValidator()
    {
        RuleFor(member => member.CvFile)
            .NotNull();
    }
}