using contatos_domain.interfaces.events;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace contatos_infrastructure.events;

public static class ConfigureEvents
{
    public static void ConfigureEventsEngine(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x => x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(configuration.GetConnectionString("RabbitMq"));

            cfg.ConfigureEndpoints(context);
        }));

        services.AddScoped(typeof(IEventProducer<>), typeof(RabbitMQEventProducer<>));

        services.AddScoped(typeof(IEventConsumer<>), typeof(RabbitMQEventConsumer<>));
    }
}
