using FluentValidation;
using RabeenApi.Dtos.Achievement.Requests;

namespace RabeenApi.Validators.Achievement;

public class UpdateAchievementRequestValidator:AbstractValidator<UpdateAchievementRequest>
{
    public UpdateAchievementRequestValidator()
    {
        RuleFor(achievement => achievement.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(achievement => achievement.Description)
            .NotEmpty()
            .MaximumLength(400);

        RuleFor(achievement => achievement.ExtraInformation)
            .Length(0,50);
    }
}