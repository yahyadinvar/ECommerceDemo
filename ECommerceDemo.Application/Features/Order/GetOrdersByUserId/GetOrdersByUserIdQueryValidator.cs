using FluentValidation;

namespace ECommerceDemo.Application.Features.Order.GetOrderByUserId;

public sealed class GetOrdersByUserIdQueryValidator : AbstractValidator<GetOrdersByUserIdQuery>
{
    public GetOrdersByUserIdQueryValidator()
    {
        RuleFor(command => command.UserId).NotEmpty().WithMessage("UserId alanı boş olamaz.");
    }
}
