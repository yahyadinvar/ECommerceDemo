namespace ECommerceDemo.Application.Features.Order.CreateOrder;

public sealed record CreateOrderCommandResponse(string UserId, string ProductId, int Quantity, int PaymentMethod);