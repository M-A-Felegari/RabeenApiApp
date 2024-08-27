using FluentValidation;
using RabeenApi.Dtos.Achievement.Requests;

namespace RabeenApi.Validators.Achievement;

public class AddAchievementRequestValidator : AbstractValidator<AddAchievementRequest>
{
    public AddAchievementRequestValidator()
    {

        RuleFor(achievement => achievement.Title)
            .NotEmpty();

        RuleFor(achievement => achievement.Description)
            .NotEmpty();

        RuleFor(achievement => achievement.ExtraInformation)
            .Length(0, 50);
    }
}