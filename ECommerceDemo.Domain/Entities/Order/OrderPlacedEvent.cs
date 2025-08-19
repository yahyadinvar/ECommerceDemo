using ECommerceDemo.Domain.Entities.Common.Base;

namespace ECommerceDemo.Domain.Entities.Order;

public class OrderPlacedEvent : DomainEvent
{
    public Guid OrderId { get; }

    public OrderPlacedEvent(Guid orderId)
    {
        OrderId = orderId;
    }
}
