using FluentValidation;
using RabeenApi.Dtos.Association.Requests;

namespace RabeenApi.Validators.Association;

public class UpdateAssociationRequestValidator:AbstractValidator<UpdateAssociationRequest>
{
    public UpdateAssociationRequestValidator()
    {
        RuleFor(association => association.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(association => association.UniversityName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(association => association.ContactLink)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(association => association.CreationDate)
            .LessThan(DateTime.Now);
    }
}