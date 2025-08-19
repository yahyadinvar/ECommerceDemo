using ECommerceDemo.Application.Abstractions.Persistence;
using ECommerceDemo.Domain;
using ECommerceDemo.Infrastructure.Persistence;
using ECommerceDemo.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceDemo.Infrastructure.Extensions.ServiceRegistration;

public static class RepositoriesExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ECommerceDemoDbContext>(options =>
        {
            var connectionString = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();

            options.UseSqlServer(connectionString!.SqlServer, sqlServerOptionsAction =>
            {
                sqlServerOptionsAction.MigrationsAssembly(typeof(InfrastructureAssembly).Assembly.FullName);
            });
        });

        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}