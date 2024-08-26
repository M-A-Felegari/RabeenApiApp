using FluentValidation;
using RabeenApi.Dtos.Association.Requests;

namespace RabeenApi.Validators.Association;

public class UpdateAssociationRequestValidator:AbstractValidator<UpdateAssociationRequest>
{
    public UpdateAssociationRequestValidator()
    {
        RuleFor(association => association.Name)
            .NotEmpty();

        RuleFor(association => association.UniversityName)
            .NotEmpty();

        RuleFor(association => association.ContactLink)
            .NotEmpty();

        RuleFor(association => association.CreationDate)
            .LessThan(DateTime.Now);
    }
}