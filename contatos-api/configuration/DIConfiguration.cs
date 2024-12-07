using contatos_application.service;
using contatos_domain.interfaces.repository;
using contatos_domain.interfaces.service;
using contatos_infrastructure.persistence.memory;
using contatos_infrastructure.persistence.mysql;
using contatos_infrastructure.persistence.rabbitMQ;
using contatos_infrastructure.persistence.text;

namespace contatos_api.Configuration;

public static class DIConfiguration
{
    public static void DIConfigure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRepositorioRegiaoDDD, RepositorioRegiaoDDD>();

        if (configuration["MemoryDatabase"] == "true")
        {
            services.AddScoped<IRepositorioContato, RepositorioMemContato>();
        }
        else
        {
            services.AddScoped<IRepositorioContato, RepositorioMySqlContato>();

            services.AddScoped(provider => ConexaoMySql.CriaConexaoMySql(provider.GetRequiredService<IConfiguration>().GetConnectionString("MySql")));
        }

        if (configuration["UseEvents"] == "true")
            services.AddScoped<IServiceContato, ServiceEventoContato>();
        else
            services.AddScoped<IServiceContato, ServiceContato>();        
    }
}
