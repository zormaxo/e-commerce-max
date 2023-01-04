﻿using FluentValidation;
using Shop.Core.Shared.Dtos;

namespace Shop.Application.Validations;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    { RuleFor(x => x.Password.Length).LessThan(10).WithMessage("FluentValidation, 5 karakterden fazla olamaz."); }
}
