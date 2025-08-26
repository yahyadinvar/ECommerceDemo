namespace ECommerceDemo.Application.Features.Order.GetOrderByUserId;

public sealed record GetOrdersByUserIdQueryResponse(string UserId, string ProductId, int Quantity, int PaymentMethod);