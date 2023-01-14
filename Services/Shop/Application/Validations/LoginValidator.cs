using FluentValidation;
using Shop.Shared.Dtos;

namespace Shop.Application.Validations;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    { RuleFor(x => x.Password.Length).GreaterThan(4).WithMessage("FluentValidation, Password must be a minimum length of '4'"); }
}
