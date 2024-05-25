using System.Net;
using CadastroContatosDomain.Entidades;

namespace CadastroContatosTests.Api;
public class TesteApiCadastro: TesteBaseDI
{
    ClientApiContatos ClientApiContatos = new(TestHtppClient);

    [Test]
    public async Task TesteIntegracao()
    {
        // Testa cadastro de registro invalido
        ContatoPessoa contatoPessoa = new ContatoPessoa() { Nome = "Teste" };
        await ClientApiContatos.Cadastrar(contatoPessoa);
        Assert.That(ClientApiContatos.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Console.WriteLine(ClientApiContatos.ApiError?.ErrorMessage);

        // Testa cadastro de registro valido
        contatoPessoa = new ContatoPessoa() { Nome = "Teste", Telefone = "11 1234-1234", EMail = "teste@cadastro.com" };
        await ClientApiContatos.Cadastrar(contatoPessoa);
        Assert.That(ClientApiContatos.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

        // Testa outro cadastro de registro valido
        contatoPessoa = new ContatoPessoa() { Nome = "Teste2", Telefone = "11 99999-9999", EMail = "teste@cadastro.com" };
        await ClientApiContatos.Cadastrar(contatoPessoa);
        Assert.That(ClientApiContatos.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

        // Testa busca por DDD (deve retornar ao menos 2 registros)
        var contatosDDD = await ClientApiContatos.BuscarPorDDD("11");
        Assert.IsTrue(contatosDDD.Length == 2);

        // Testa alteração de contato por nome
        contatoPessoa = contatosDDD[1];
        contatoPessoa.Telefone = "13 1111-1111";
        await ClientApiContatos.Alterar(contatoPessoa);
        Assert.That(ClientApiContatos.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        // Assegura que alteração acima foi bem sucedida (apenas um registro no ddd 11)
        contatosDDD = await ClientApiContatos.BuscarPorDDD("11");
        Assert.IsTrue(contatosDDD.Length == 1);

        // Testa exclusão contato
        await ClientApiContatos.Excluir("Teste");
        Assert.That(ClientApiContatos.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        
        // Assegura que alteração acima foi bem sucedida
        contatosDDD = await ClientApiContatos.BuscarPorDDD("11");
        Assert.IsTrue(contatosDDD.Length == 0);

    }
}
