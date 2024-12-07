using contatos_application.service;
using contatos_domain.interfaces.service;
using contatos_infrastructure.persistence.mysql;
using contatos_infrastructure.persistence.text;
using contatos_testes_integration.fixture;
using contatos_tests.service;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace contatos_testes_integration.infrastructure.mySql;

public class TestesRepositorioMySQLContato(ConfigurationFixture configFixture) : IClassFixture<ConfigurationFixture>
{
    private readonly IConfiguration _configuration = configFixture.Configuration;

    MySqlConnection ConectarMySql() => ConexaoMySql.CriaConexaoMySql(_configuration.GetConnectionString("MySql"));

    [Fact]
    public void TestConnection()
    {
        ConectarMySql();
    }

    [Fact]
    public async Task TestServiceContato()
    {
        var conexaoMySql = ConectarMySql();

        IServiceContato serviceContato = new ServiceContato(new RepositorioMySqlContato(conexaoMySql, new RepositorioRegiaoDDD()));

        ServiceContatoTester serviceContatoTester = new ServiceContatoTester();

        var transaction = conexaoMySql.BeginTransaction();
        try
        {
            await serviceContatoTester.TestServiceContato(serviceContato);
        }
        finally
        {
            transaction.Rollback();
        }
    }
}
