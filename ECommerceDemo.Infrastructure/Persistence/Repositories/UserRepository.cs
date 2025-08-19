using ECommerceDemo.Application.Abstractions.Persistence;
using ECommerceDemo.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace ECommerceDemo.Infrastructure.Persistence.Repositories;

public class UserRepository : GenericRepository<User, Guid>, IUserRepository
{
    private readonly ECommerceDemoDbContext _dbContext;
    public UserRepository(ECommerceDemoDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}

