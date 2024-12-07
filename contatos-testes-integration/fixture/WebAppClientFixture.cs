using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace contatos_testes_integration.fixture;

public class WebAppClientFixture
{
    public HttpClient TestHtppClient { get; }
    public IServiceProvider ServiceProvider { get; }
    public IConfiguration Configuration { get; }

    public WebAppClientFixture()
    {
        var factory = new WebApplicationFactory<Program>();
        TestHtppClient = factory.CreateClient();
        ServiceProvider = factory.Services;
        Configuration = ServiceProvider.GetRequiredService<IConfiguration>();
    }
}
