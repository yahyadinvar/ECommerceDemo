using ECommerceDemo.Domain;
using ECommerceDemo.Infrastructure.Messaging;
using ECommerceDemo.Infrastructure.Persistence;
using ECommerceDemo.Worker.Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ECommerceDemo.Worker.Extensions;

public static class MessagingExtension
{
    public static IServiceCollection AddRabbitMqConsumers(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ECommerceDemoDbContext>(options =>
        {
            var connectionString = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
            options.UseSqlServer(connectionString!.SqlServer);
        });

        var rabbitSection = configuration.GetSection("RabbitMqOptions");
        services.Configure<RabbitMqOptions>(rabbitSection);
        var options = rabbitSection.Get<RabbitMqOptions>();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<OrderPlacedIntegrationEventConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(options.Url), h => { });

                cfg.ReceiveEndpoint("order-placed-queue", e =>
                {
                    e.ConfigureConsumer<OrderPlacedIntegrationEventConsumer>(context);
                });
            });
        });

        return services;
    }
}
