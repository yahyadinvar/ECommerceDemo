using FluentValidation;

namespace ECommerceDemo.Application.Features.Auth.Register;
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName)
           .NotEmpty().WithMessage("Ad alanı zorunlu.")
           .MaximumLength(100).WithMessage("Ad alanı max 100 karakter olmalı.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad alanı zorunlu.")
            .MaximumLength(100).WithMessage("Soyad alanı max 100 karakter olmalı.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email alanı zorunlu.")
            .EmailAddress().WithMessage("A valid email address is required.")
            .MaximumLength(150).WithMessage("Email alanı max 150 karakter olmalı.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre alanı zorunlu.")
            .MinimumLength(6).WithMessage("Şifre alanı min 6 karakter olmalı.");
    }
}
