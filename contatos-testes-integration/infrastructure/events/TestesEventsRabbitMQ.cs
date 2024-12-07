using contatos_domain.dto;
using contatos_domain.entidades;
using contatos_domain.interfaces.events;
using contatos_domain.interfaces.repository;
using contatos_infrastructure.events;
using contatos_infrastructure.persistence.memory;
using contatos_testes_integration.fixture;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace contatos_testes_integration.infrastructure.events;

public class TestesEventsRabbitMQ(ConfigurationFixture configFixture) : IClassFixture<ConfigurationFixture>
{
    public class TestEvent
    {
        public string Id { get; set; }
    }

    [Fact]
    public async Task TesteEventsRabbitMQ()
    {
        IServiceCollection services = new ServiceCollection();
        services.ConfigureEventsEngine(configFixture.Configuration);
        var serviceProvider = services.BuildServiceProvider();

        IEventConsumer<TestEvent> eventConsumer = serviceProvider.GetService<IEventConsumer<TestEvent>>();

        bool consumed = false;

        await eventConsumer.ConsumeAsync(async (message) =>
        {
            consumed = true;
            Console.WriteLine($"Receiving message {message.Id}");
        });

        IEventProducer<TestEvent> eventProducer = serviceProvider.GetService<IEventProducer<TestEvent>>();

        var test = new TestEvent() { Id = Guid.NewGuid().ToString() };
        Console.WriteLine($"Publishing message {test.Id}");
        await eventProducer.PublishAsync(test);

        while (!consumed)
        {
            await Task.Delay(1000);
        }
    }

    [Fact]
    public async Task TesteEventoCadastroProcess()
    {
        IServiceCollection services = new ServiceCollection();
        services.ConfigureEventsEngine(configFixture.Configuration);
        var serviceProvider = services.BuildServiceProvider();
        IEventConsumer<EventoCadastroContatoPessoa> eventConsumer = serviceProvider.GetService<IEventConsumer<EventoCadastroContatoPessoa>>();

        IRepositorioContato repositorioContato = new RepositorioMemContato(new RepositorioMemRegiaoDDD());

        EventoCadastroContatoProcess eventoCadastroContatoProcess = new EventoCadastroContatoProcess(repositorioContato, eventConsumer);

        IEventProducer<EventoCadastroContatoPessoa> eventProducer = serviceProvider.GetService<IEventProducer<EventoCadastroContatoPessoa>>();

        EventoCadastroContatoPessoa eventoCadastro = new(TipoEventoCadastro.Cadastro, new ContatoPessoa() { Nome = "Teste", Telefone = "11 1234-1234", EMail = "teste@cadastro.com" });
        await eventProducer.PublishAsync(eventoCadastro);

        await Task.Delay(2000);

        var res = await repositorioContato.BuscaPorDDD("11");
        Assert.Single(res);       
    }
}
