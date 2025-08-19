namespace ECommerceDemo.Application.Abstractions.Services;
public interface ICurrentUserService
{
    Guid? UserId { get; }
}

