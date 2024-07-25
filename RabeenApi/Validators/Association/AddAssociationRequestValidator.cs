using FluentValidation;
using RabeenApi.Dtos.Association.Requests;

namespace RabeenApi.Validators.Association;

public class AddAssociationRequestValidator : AbstractValidator<AddAssociationRequest>
{
    public AddAssociationRequestValidator()
    {
        RuleFor(association => association.Name)
            .NotEmpty();

        RuleFor(association => association.UniversityName)
            .NotEmpty();

        RuleFor(association => association.ContactLink)
            .NotEmpty();

        RuleFor(association => association.CreationDate)
            .LessThan(DateTime.Now);

        RuleFor(association => association.FirstCooperationDate)
            .LessThanOrEqualTo(DateTime.Now);
    }
}