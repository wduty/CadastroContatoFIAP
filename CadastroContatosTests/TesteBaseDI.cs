using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CadastroContatosTests;

public class TesteBaseDI
{
    protected static HttpClient TestHtppClient;
    static IServiceProvider ServiceProvider;
    protected static IConfiguration Configuration;

    static TesteBaseDI()
    {
        var factory = new WebApplicationFactory<Program>();
        TestHtppClient = factory.CreateClient();
        ServiceProvider = factory.Services;
        Configuration = ServiceProvider.GetRequiredService<IConfiguration>();
    }

    protected IServiceScope ServiceScope;

    public TesteBaseDI()
    {
        ServiceScope = ServiceProvider.CreateScope();
    }

    protected T GetService<T>() => ServiceScope.ServiceProvider.GetRequiredService<T>();
}