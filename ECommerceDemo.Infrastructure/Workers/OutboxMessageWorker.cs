using ECommerceDemo.Application.Abstractions.Messaging;
using ECommerceDemo.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ECommerceDemo.Infrastructure.Workers;

public class OutboxMessageWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OutboxMessageWorker> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(30); // Her 30 saniyede bir çalışır

    public OutboxMessageWorker(IServiceProvider serviceProvider, ILogger<OutboxMessageWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("OutboxMessageWorker started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<ECommerceDemoDbContext>();
                var publisher = scope.ServiceProvider.GetRequiredService<IMessagePublisher>();

                var pendingMessages = dbContext.OutboxMessages
                    .Where(m => !m.IsPublished)
                    .OrderBy(m => m.OccurredOn)
                    .Take(20)
                    .ToList();

                foreach (var message in pendingMessages)
                {
                    var eventType = Type.GetType(message.Type);
                    if (eventType == null)
                    {
                        _logger.LogWarning("Unknown event type: {Type}", message.Type);
                        continue;
                    }

                    var @event = JsonConvert.DeserializeObject(message.Content, eventType);
                    if (@event == null)
                    {
                        _logger.LogWarning("Deserialization failed for message ID: {Id}", message.Id);
                        continue;
                    }

                    await publisher.Publish(@event); // Örn: MassTransit ile publish edilir

                    message.IsPublished = true;
                    message.PublishedOn = DateTime.UtcNow;
                }

                await dbContext.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while publishing outbox messages.");
            }

            await Task.Delay(_interval, stoppingToken);
        }

        _logger.LogInformation("OutboxMessageWorker stopped.");
    }
}