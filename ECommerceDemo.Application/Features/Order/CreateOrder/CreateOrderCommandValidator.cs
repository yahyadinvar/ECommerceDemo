using FluentValidation;

namespace ECommerceDemo.Application.Features.Order.CreateOrder;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.UserId)
                 .NotEmpty().WithMessage("UserId alanı boş olamaz.");

        RuleFor(x => x.ProductId)
               .NotEmpty().WithMessage("ProductId alanı boş olamaz.");

        RuleFor(x => x.Quantity)
               .NotEmpty().WithMessage("Quantity alanı boş olamaz.");

        RuleFor(x => x.PaymentMethod)
               .NotEmpty().WithMessage("paymentMethod alanı boş olamaz.");
    }
}
