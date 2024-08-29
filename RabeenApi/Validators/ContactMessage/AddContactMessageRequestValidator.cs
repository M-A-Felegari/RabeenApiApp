using FluentValidation;
using RabeenApi.Dtos.ContactMessage.Requests;

namespace RabeenApi.Validators.ContactMessage;

public class AddContactMessageRequestValidator : AbstractValidator<AddContactMessageRequest>
{
    public AddContactMessageRequestValidator()
    {
        RuleFor(message => message.Email)
            .EmailAddress()
            .MaximumLength(320);

        RuleFor(message => message.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(message => message.Subject)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(message => message.Text)
            .NotEmpty()
            .MaximumLength(1000);
    }
}