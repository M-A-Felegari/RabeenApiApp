using FluentValidation;
using RabeenApi.Dtos.Association.Requests;

namespace RabeenApi.Validators.Association;

public class SetAssociationLogoRequestValidator:AbstractValidator<SetAssociationLogoRequest>
{
    public SetAssociationLogoRequestValidator()
    {
        RuleFor(association => association.Logo)
            .NotNull();
    }
}