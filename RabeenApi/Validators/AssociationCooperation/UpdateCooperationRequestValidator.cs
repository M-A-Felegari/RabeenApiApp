using FluentValidation;
using RabeenApi.Dtos.AssociationCooperation.Requests;

namespace RabeenApi.Validators.AssociationCooperation;

public class UpdateCooperationRequestValidator:AbstractValidator<UpdateCooperationRequest>
{
    public UpdateCooperationRequestValidator()
    {
        RuleFor(cooperation => cooperation.Title)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(cooperation => cooperation.Description)
            .NotEmpty()
            .MaximumLength(650);

        RuleFor(cooperation => cooperation.StartDate)
            .LessThan(DateTime.Now);

        RuleFor(cooperation => cooperation.FinishDate)
            .LessThanOrEqualTo(DateTime.Now)
            .GreaterThan(cooperation => cooperation.StartDate);
    }
}