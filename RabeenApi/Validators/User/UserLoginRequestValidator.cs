﻿using FluentValidation;
using RabeenApi.Dtos.User.Requests;

namespace RabeenApi.Validators.User;

public class UserLoginRequestValidator:AbstractValidator<UserLoginRequest>
{
    public UserLoginRequestValidator()
    {
        RuleFor(l => l.Username)
            .Length(6,20);

        RuleFor(l => l.Password)
            .Length(8, 16);
    }
}