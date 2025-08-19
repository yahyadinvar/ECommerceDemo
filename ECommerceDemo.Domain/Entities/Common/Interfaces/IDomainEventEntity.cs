using ECommerceDemo.Domain.Entities.Common.Base;

namespace ECommerceDemo.Domain.Entities.Common.Interfaces;
public interface IDomainEventEntity
{
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
