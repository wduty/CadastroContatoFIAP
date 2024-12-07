using Microsoft.Extensions.Configuration;

namespace contatos_testes_integration.fixture;

public class ConfigurationFixture
{
    public IConfiguration Configuration { get; }

    public ConfigurationFixture()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory) // Ensures the correct path
            .AddJsonFile("appsettings.test.json", optional: false, reloadOnChange: false)
            .Build();
    }
}