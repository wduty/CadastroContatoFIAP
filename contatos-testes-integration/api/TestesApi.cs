using contatos_domain.entidades;
using contatos_testes_integration.fixture;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace contatos_testes_integration.api;

public class TestesApi(WebAppClientFixture webAppClientFixture) : IClassFixture<WebAppClientFixture>
{
    private readonly HttpClient TestHtppClient = webAppClientFixture.TestHtppClient;
    private readonly IConfiguration Configuration = webAppClientFixture.Configuration;

    [Fact]
    public async Task TestFluxoApi()
    {
        ClientApiContatos clientApiContatos = new ClientApiContatos(TestHtppClient);

        // Testa cadastro de registro invalido
        ContatoPessoa contatoPessoa = new ContatoPessoa() { Nome = "Teste" };
        await clientApiContatos.Cadastrar(contatoPessoa);
        Assert.Equal(HttpStatusCode.BadRequest, clientApiContatos.StatusCode);
        Console.WriteLine(clientApiContatos.ApiError?.ErrorMessage);

        // Testa cadastro de registro valido
        contatoPessoa = new ContatoPessoa() { Nome = "Teste", Telefone = "11 1234-1234", EMail = "teste@cadastro.com" };
        await clientApiContatos.Cadastrar(contatoPessoa);
        Assert.Equal(HttpStatusCode.NoContent, clientApiContatos.StatusCode);

        // Testa outro cadastro de registro valido
        contatoPessoa = new ContatoPessoa() { Nome = "Teste2", Telefone = "11 99999-9999", EMail = "teste@cadastro.com" };
        await clientApiContatos.Cadastrar(contatoPessoa);
        Assert.Equal(HttpStatusCode.NoContent, clientApiContatos.StatusCode);

        // Testa busca por DDD (deve retornar ao menos 2 registros)
        var contatosDDD = await clientApiContatos.BuscarPorDDD("11");
        Assert.True(contatosDDD.Length == 2);

        // Testa alteração de contato por nome
        contatoPessoa = contatosDDD[1];
        contatoPessoa.Telefone = "13 1111-1111";
        await clientApiContatos.Alterar(contatoPessoa);
        Assert.Equal(HttpStatusCode.OK, clientApiContatos.StatusCode);

        // Assegura que alteração acima foi bem sucedida (apenas um registro no ddd 11)
        contatosDDD = await clientApiContatos.BuscarPorDDD("11");
        Assert.True(contatosDDD.Length == 1);

        // Testa exclusão contato
        await clientApiContatos.Excluir("Teste");
        Assert.Equal(HttpStatusCode.OK, clientApiContatos.StatusCode);

        // Assegura que alteração acima foi bem sucedida
        contatosDDD = await clientApiContatos.BuscarPorDDD("11");
        Assert.True(contatosDDD.Length == 0);

    }
}
