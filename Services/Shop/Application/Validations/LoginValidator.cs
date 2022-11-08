using Core.Dtos;
using FluentValidation;

namespace BP.Api.Validations
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        { RuleFor(x => x.Password.Length).LessThan(5).WithMessage("FluentValidation, 5 karakterden fazla olamaz."); }
    }
}
