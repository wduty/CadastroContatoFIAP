namespace contatos_domain.interfaces.events;

public interface IEventProducer<T> where T : class
{
    Task PublishAsync(T message);
}
