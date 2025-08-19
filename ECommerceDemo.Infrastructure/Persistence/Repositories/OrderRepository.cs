using ECommerceDemo.Application.Abstractions.Persistence;
using ECommerceDemo.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;

namespace ECommerceDemo.Infrastructure.Persistence.Repositories;

public class OrderRepository : GenericRepository<Order, Guid>, IOrderRepository
{
    private readonly ECommerceDemoDbContext _dbContext;
    public OrderRepository(ECommerceDemoDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Order> GetOrderByUserIdAsync(string userId)
    {
        return await _dbContext.Orders.FirstOrDefaultAsync(u => u.UserId == userId);
    }

}
