using contatos_domain.dto;
using contatos_domain.entidades;
using contatos_domain.interfaces.events;
using contatos_domain.interfaces.repository;
using contatos_infrastructure.events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

IServiceCollection services = new ServiceCollection();

//IConfiguration configuration = new ConfigurationBuilder()
//    .SetBasePath(AppContext.BaseDirectory) // Ensures the correct path
//    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
//    .Build();

//services.ConfigureEventsEngine(configuration);

//var serviceProvider = services.BuildServiceProvider();

//IEventConsumer eventConsumer = serviceProvider.GetService<IEventConsumer>();

//EventoCadastroContatoProcess eventoCadastroProcess = new EventoCadastroContatoProcess(serviceProvider.GetService<IRepositorioContato>());

//async Task EventCadastro(EventoCadastroContatoPessoa eventoCadastro)
//{
//    await eventoCadastroProcess.ProcessarEventoCadastro(eventoCadastro);
//}

//await eventConsumer.ConsumeAsync<EventoCadastroContatoPessoa>(EventCadastro);