using FluentValidation;
using RabeenApi.Dtos.Association.Requests;

namespace RabeenApi.Validators.Association;

public class GetAllAssociationsRequestValidator : AbstractValidator<GetAllAssociationsRequest>
{
    public GetAllAssociationsRequestValidator()
    {
        RuleFor(request => request.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(request => request.PageLength)
            .GreaterThanOrEqualTo(1);
    }
}