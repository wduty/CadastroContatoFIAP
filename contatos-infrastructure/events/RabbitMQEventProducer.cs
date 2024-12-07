using contatos_domain.interfaces.events;
using MassTransit;

namespace contatos_infrastructure.events;

public class RabbitMQEventProducer<T>(IBus bus) : IEventProducer<T> where T : class
{
    public async Task PublishAsync(T message) 
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        await bus.Publish(message);
    }
}
