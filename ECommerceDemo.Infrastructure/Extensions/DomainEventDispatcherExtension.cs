using ECommerceDemo.Domain.Entities.Common.Interfaces;
using ECommerceDemo.Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ECommerceDemo.Infrastructure.Extensions;


public static class DomainEventDispatcherExtension
{
    public static void AddDomainEventsToOutbox(this DbContext context, CancellationToken cancellationToken = default)
    {
        var domainEntities = context.ChangeTracker
          .Entries<IDomainEventEntity>()
          .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
          .ToList();

        var outboxMessages = new List<OutboxMessage>();
        var outboxSet = context.Set<OutboxMessage>();

        foreach (var entityEntry in domainEntities)
        {
            foreach (var domainEvent in entityEntry.Entity.DomainEvents)
            {
                var outboxMessage = new OutboxMessage
                {
                    Id = Guid.NewGuid(),
                    Type = domainEvent.GetType().AssemblyQualifiedName ?? domainEvent.GetType().FullName!,
                    OccurredOn = domainEvent.OccurredOn,
                    Content = JsonConvert.SerializeObject(domainEvent),
                    IsPublished = false
                };

                outboxMessages.Add(outboxMessage);
            }

            entityEntry.Entity.ClearDomainEvents();
        }

        outboxSet.AddRange(outboxMessages);
    }
}