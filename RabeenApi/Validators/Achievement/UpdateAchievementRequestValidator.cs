using FluentValidation;
using RabeenApi.Dtos.Achievement.Requests;

namespace RabeenApi.Validators.Achievement;

public class UpdateAchievementRequestValidator:AbstractValidator<UpdateAchievementRequest>
{
    public UpdateAchievementRequestValidator()
    {
        RuleFor(achievement => achievement.Title)
            .NotEmpty();

        RuleFor(achievement => achievement.Description)
            .NotEmpty();

        RuleFor(achievement => achievement.Date)
            .LessThan(DateTime.Now);
    }
}