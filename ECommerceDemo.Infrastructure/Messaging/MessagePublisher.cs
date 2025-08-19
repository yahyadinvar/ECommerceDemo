using ECommerceDemo.Application.Abstractions.Messaging;
using MassTransit;

namespace ECommerceDemo.Infrastructure.Messaging;

public class MessagePublisher : IMessagePublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MessagePublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task Publish(object @event)
    {
        // Outbox'tan publish event runtime'da generic gelmediği için (string geliyor), reflection ile yapıldı.

        if (@event == null)
            throw new ArgumentNullException(nameof(@event));

        // Runtime'de öğrendiğimiz tip
        var method = typeof(IPublishEndpoint)
            .GetMethods()
            .Where(m => m.Name == "Publish")
            .FirstOrDefault(m =>
            {
                var parameters = m.GetParameters();
                return parameters.Length == 1 || parameters.Length == 2;
            });

        if (method == null)
            throw new InvalidOperationException("Publish method not found");

        // Generic metodu, runtime’da öğrendiğimiz tip ile oluşturuldu.
        var genericMethod = method.MakeGenericMethod(@event.GetType());

        // Oluşturduğumuz generic metod, parametre sayısına göre çağrıldı.
        var parameters = method.GetParameters().Length == 1
            ? new object[] { @event }
            : new object[] { @event, CancellationToken.None };

        await (Task)genericMethod.Invoke(_publishEndpoint, parameters)!;
    }
}
