using ECommerceDemo.Application.Abstractions;

namespace ECommerceDemo.Application.Features.Order.CreateOrder;

public sealed record CreateOrderCommand(string UserId, string ProductId, int Quantity, int PaymentMethod) : ICommand<CreateOrderCommandResponse>;