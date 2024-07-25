using FluentValidation;
using RabeenApi.Dtos.AssociationCooperation.Requests;

namespace RabeenApi.Validators.AssociationCooperation;

public class GetAllAssociationCooperationsRequestValidator:AbstractValidator<GetAllAssociationCooperationsRequest>
{
    public GetAllAssociationCooperationsRequestValidator()
    {
        RuleFor(request => request.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(request => request.PageLength)
            .GreaterThanOrEqualTo(1);
    }
}