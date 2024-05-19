using CadastroContatosApplication.Service;
using CadastroContatosDomain.Interfaces.Repositorio;
using CadastroContatosDomain.Interfaces.Service;
using CadastroContatosInfrastructure.Repository;

namespace CadastroContatosApi.ServiceConfig;

public static class DependecyInjectionConfig
{
    public static IServiceCollection WireDependencyInjections(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddScoped<IRepositorioRegiaoDDD, RepositorioRegiaoDDD>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IServiceContato, ServiceContatos>();

        return services;
    }
}
