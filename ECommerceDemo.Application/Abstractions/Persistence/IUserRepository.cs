using ECommerceDemo.Domain.Entities.User;

namespace ECommerceDemo.Application.Abstractions.Persistence;

public interface IUserRepository: IGenericRepository<User, Guid>
{
    Task<User> GetUserByEmailAsync(string email);
}
