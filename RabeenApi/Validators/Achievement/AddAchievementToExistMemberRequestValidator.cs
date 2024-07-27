using FluentValidation;
using RabeenApi.Dtos.Achievement.Requests;

namespace RabeenApi.Validators.Achievement;

public class AddAchievementToExistMemberRequestValidator : AbstractValidator<AddAchievementRequest>
{
    public AddAchievementToExistMemberRequestValidator()
    {
        RuleFor(achievement => achievement.Title)
            .NotEmpty();

        RuleFor(achievement => achievement.Description)
            .NotEmpty();

        RuleFor(achievement => achievement.Date)
            .LessThanOrEqualTo(DateTime.Now);
    }
}