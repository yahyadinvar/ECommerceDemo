using ECommerceDemo.Application.Abstractions.Persistence;
using ECommerceDemo.Domain.Entities.Order;
using ECommerceDemo.Infrastructure.Persistence;
using MassTransit;

namespace ECommerceDemo.Worker.Consumers;

public class OrderPlacedIntegrationEventConsumer : IConsumer<OrderPlacedEvent>
{
    private readonly ECommerceDemoDbContext _dbContext;
    private readonly ILogger<OrderPlacedIntegrationEventConsumer> _logger;
    private readonly ICacheRepository _cacheRepository;

    public OrderPlacedIntegrationEventConsumer(ECommerceDemoDbContext dbContext, ILogger<OrderPlacedIntegrationEventConsumer> logger, ICacheRepository cacheRepository)
    {
        _dbContext = dbContext;
        _logger = logger;
        _cacheRepository = cacheRepository;
    }
    public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
    {
        var @event = context.Message;

        _logger.LogInformation($"[OrderPlacedIntegrationEventConsumer] OrderPlacedEvent received for OrderId: {@event.OrderId}");

        await _cacheRepository.SetAsync("processed-orders", $"Order {@event.OrderId} processed at {DateTime.UtcNow}", TimeSpan.FromDays(1));

        _logger.LogInformation($"[OrderPlacedIntegrationEventConsumer] Redis log written for order: {@event.OrderId}");

        await _dbContext.SaveChangesAsync();
    }
}
