using FluentValidation;

namespace ECommerceDemo.Application.Features.Order.GetOrderByUserId;

public sealed class GetOrderByUserIdQueryValidator : AbstractValidator<GetOrderByUserIdQuery>
{
    public GetOrderByUserIdQueryValidator()
    {
        RuleFor(command => command.UserId).NotEmpty().WithMessage("UserId alanı boş olamaz.");
    }
}
