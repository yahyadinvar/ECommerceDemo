using ECommerceDemo.Domain.Entities.Order;

namespace ECommerceDemo.Application.Abstractions.Persistence;

public interface IOrderRepository : IGenericRepository<Order, Guid>
{
    Task<Order> GetOrderByUserIdAsync(string userId);
}
