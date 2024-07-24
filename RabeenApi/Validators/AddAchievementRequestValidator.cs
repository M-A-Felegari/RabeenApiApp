using FluentValidation;
using RabeenApi.Dtos.Achievement.Requests;

namespace RabeenApi.Validators;

public class AddAchievementRequestValidator : AbstractValidator<AddAchievementRequest>
{
    public AddAchievementRequestValidator()
    {
        RuleFor(achievement => achievement.Title)
            .NotEmpty()
            .MaximumLength(60);
        
        RuleFor(achievement => achievement.Description)
            .NotEmpty()
            .MaximumLength(180);
        
        RuleFor(achievement => achievement.Date)
            .NotNull();
    }
}