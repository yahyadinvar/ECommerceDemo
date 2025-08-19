using ECommerceDemo.Domain.Entities.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceDemo.Domain.Entities.Common.Base;

public abstract class BaseEntity<TKey> : IEntity<TKey>, IDomainEventEntity
{
    public TKey Id { get; set; } = default;

    private List<DomainEvent> _domainEvents;

    [NotMapped]
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

    public void AddDomainEvent(DomainEvent eventItem)
    {
        _domainEvents ??= new List<DomainEvent>();
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}

