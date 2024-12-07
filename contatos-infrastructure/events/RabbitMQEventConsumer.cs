using contatos_domain.interfaces.events;
using MassTransit;

namespace contatos_infrastructure.events;

public class RabbitMQEventConsumer<T>(IBusControl bus) : IEventConsumer<T> where T : class
{
    public async Task ConsumeAsync(Func<T, Task> onMessageReceived, CancellationToken cancellationToken = default) 
    {
        var handle = bus.ConnectReceiveEndpoint(typeof(T).Name, e =>
        {
            e.Handler<T>(async context =>
            {
                await onMessageReceived(context.Message);
            });
        });

        await bus.StartAsync(cancellationToken);
        await handle.Ready;

        cancellationToken.Register(() => bus.StopAsync());
    }
}
