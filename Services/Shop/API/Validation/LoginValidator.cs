﻿using FluentValidation;
using Shop.Shared.Dtos.Account;

namespace Shop.API.Validation;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Password.Length)
            .GreaterThanOrEqualTo(4)
            .WithMessage("FluentValidation, Password must be a minimum length of '4'");
    }
}
