using ECommerceDemo.Application.Abstractions.Services;

namespace ECommerceDemo.Worker.Services;

public class FakeCurrentUserService : ICurrentUserService
{
    Guid? ICurrentUserService.UserId => Guid.Parse("00000000-0000-0000-0000-000000000000");
}