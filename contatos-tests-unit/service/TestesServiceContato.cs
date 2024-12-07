using contatos_application.service;
using contatos_domain.entidades;
using contatos_domain.interfaces.service;
using contatos_infrastructure.persistence.memory;

namespace contatos_tests.service;

public class TestesServiceContato
{
    [Fact]
    public async Task TesteServiceContato()
    {
        IServiceContato serviceContato = new ServiceContato(new RepositorioMemContato(new RepositorioMemRegiaoDDD()));
        await new ServiceContatoTester().TestServiceContato(serviceContato);
    }
}

public class ServiceContatoTester
{
    public async Task TestServiceContato(IServiceContato serviceContato)
    {
        await Assert.ThrowsAsync<ArgumentException>(async () => await serviceContato.CadastrarContato(new ContatoPessoa()));

        await serviceContato.CadastrarContato(new ContatoPessoa() { Nome = "João", Telefone = "11 9999-9999", EMail = "joao@teste.com" });
        await serviceContato.CadastrarContato(new ContatoPessoa() { Nome = "Maria", Telefone = "11 8888-8888", EMail = "maria@teste.com" });
        await serviceContato.CadastrarContato(new ContatoPessoa() { Nome = "Mario", Telefone = "11 1111-1111", EMail = "maria@teste.com" });
        await serviceContato.CadastrarContato(new ContatoPessoa() { Nome = "Carlos", Telefone = "12 7777-7777", EMail = "carlos@teste.com" });

        var contatos = await serviceContato.BuscarPorDDD("11");
        Assert.Equal(3, contatos.Count());

        await serviceContato.ExcluirContato("Maria");

        contatos = await serviceContato.BuscarPorDDD("11");
        Assert.Equal(2, contatos.Count());

        await serviceContato.AlterarContato(new ContatoPessoa() { Nome = "Mario", Telefone = "12 2222-2222", EMail = "teste@x.com" });

        contatos = await serviceContato.BuscarPorDDD("11");
        Assert.Single(contatos);
    }
}