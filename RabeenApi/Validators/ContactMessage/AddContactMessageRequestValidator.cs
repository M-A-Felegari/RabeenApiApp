using FluentValidation;
using RabeenApi.Dtos.ContactMessage.Requests;

namespace RabeenApi.Validators.ContactMessage;

public class AddContactMessageRequestValidator : AbstractValidator<AddContactMessageRequest>
{
    public AddContactMessageRequestValidator()
    {
        RuleFor(message => message.Email)
            .EmailAddress();

        RuleFor(message => message.Name)
            .NotEmpty();

        RuleFor(message => message.Subject)
            .NotEmpty();

        RuleFor(message => message.Text)
            .NotEmpty();
    }
}