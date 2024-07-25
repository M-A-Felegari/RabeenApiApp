using FluentValidation;
using RabeenApi.Dtos.ContactMessage.Requests;

namespace RabeenApi.Validators.ContactMessage;

public class GetAllContactMessagesRequestValidator:AbstractValidator<GetAllContactMessagesRequest>
{
    public GetAllContactMessagesRequestValidator()
    {
        RuleFor(request => request.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(request => request.PageLength)
            .GreaterThanOrEqualTo(1);
    }
}