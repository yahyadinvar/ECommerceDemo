namespace ECommerceDemo.Application.Abstractions.Messaging;
public interface IMessagePublisher
{
    Task Publish(object @event);
}

