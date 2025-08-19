using ECommerceDemo.Application.Abstractions.Messaging;
using ECommerceDemo.Infrastructure.Messaging;
using ECommerceDemo.Infrastructure.Workers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceDemo.Infrastructure.Extensions.ServiceRegistration;

public static class MessagingExtension
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitSection = configuration.GetSection("RabbitMqOptions");
        services.Configure<RabbitMqOptions>(rabbitSection);
        var options = rabbitSection.Get<RabbitMqOptions>();

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(options.Url), h => { });
            });
        });

        services.AddHostedService<OutboxMessageWorker>();
        services.AddScoped<IMessagePublisher, MessagePublisher>();

        return services;
    }
}
