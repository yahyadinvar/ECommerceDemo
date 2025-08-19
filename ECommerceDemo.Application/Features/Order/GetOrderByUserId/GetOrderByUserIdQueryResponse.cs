namespace ECommerceDemo.Application.Features.Order.GetOrderByUserId;

public sealed record GetOrderByUserIdQueryResponse(string UserId, string ProductId, int Quantity, int PaymentMethod);