namespace contatos_domain.interfaces.events;

public interface IEventConsumer<T> where T : class
{
    Task ConsumeAsync(Func<T, Task> onMessageReceived, CancellationToken cancellationToken = default);
}
