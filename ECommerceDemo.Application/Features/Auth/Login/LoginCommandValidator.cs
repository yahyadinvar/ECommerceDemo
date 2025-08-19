using FluentValidation;

namespace ECommerceDemo.Application.Features.Auth.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email alanı zorunludur.")
           .EmailAddress().WithMessage("Geçerli bir e-posta adresi gereklidir.")
           .MaximumLength(150).WithMessage("Email alanı max 150 karakter olmalıdır.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre alanı zorunludur.")
            .MinimumLength(6).WithMessage("Şifre alanı min 6 karakter olmalıdır.");
    }
}