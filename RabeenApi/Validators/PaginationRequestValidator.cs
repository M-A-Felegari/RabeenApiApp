using FluentValidation;
using RabeenApi.Dtos;

namespace RabeenApi.Validators;

public class PaginationRequestValidator : AbstractValidator<PaginationRequest>
{
    public PaginationRequestValidator()
    {
        RuleFor(p => p.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(p => p.PageLength)
            .GreaterThanOrEqualTo(1);
    }
}