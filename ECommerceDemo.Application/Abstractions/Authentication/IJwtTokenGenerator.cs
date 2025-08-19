using ECommerceDemo.Domain.Entities.User;

namespace ECommerceDemo.Application.Abstractions.Authentication;
public interface IJwtTokenGenerator
{
    public string GenerateToken(User user);
}

