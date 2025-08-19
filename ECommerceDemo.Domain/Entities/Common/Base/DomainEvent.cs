namespace ECommerceDemo.Domain.Entities.Common.Base;

public abstract class DomainEvent
{
    public DateTime OccurredOn { get; protected set; } = DateTime.UtcNow;
}
