using CadastroContatosDomain.Entidades;
using CadastroContatosDomain.Interfaces.Repositorio;

namespace CadastroContatosTests.Infrastructure;

public class TesteRepositorioContato: TesteBaseDI
{
    [Test]
    public async Task TesteCadastroContato()
    {
        using (IUnitOfWork unitOfWork = GetService<IUnitOfWork>())
        {
            Assert.ThrowsAsync<ArgumentException>(() => unitOfWork.RepositorioContato.CadastrarContato(new Contato()));
            Assert.ThrowsAsync<ArgumentException>(() => unitOfWork.RepositorioContato.CadastrarContato(new Contato() { Nome = "Teste" }));

            try
            {
                unitOfWork.BeginTransaction();

                await unitOfWork.RepositorioContato.CadastrarContato(new Contato() { Nome = "teste", Telefone = "11 91234-1234", EMail = "teste@teste.com" });
                await unitOfWork.RepositorioContato.CadastrarContato(new Contato() { Nome = "teste 2", Telefone = "11 9999-9999", EMail = "teste2@teste.com" });

                var contatos = await unitOfWork.RepositorioContato.BuscaPorDDD("11");

                Assert.IsTrue(contatos.Count() > 0);
                Assert.IsTrue(contatos.All(c => c.RegiaoDDD?.DDD == "11"));
            }
            finally
            {
                unitOfWork.Rollback();
            }
        }
    }

    [Test]
    public async Task TestAlteracaoContato()
    {
        using (IUnitOfWork unitOfWork = GetService<IUnitOfWork>())
        {

            try
            {
                unitOfWork.BeginTransaction();

                Contato contato = new Contato() { Nome = "teste", Telefone = "11 91234-1234", EMail = "teste@teste.com" };
                await unitOfWork.RepositorioContato.CadastrarContato(contato);

                contato.Telefone = "11 9999-9999";
                contato.EMail = "novo@teste.com";

                await unitOfWork.RepositorioContato.AlterarContato(contato);

                var contatoAlterado = (await unitOfWork.RepositorioContato.BuscaPorDDD("11")).First(c => c.Nome == "teste");

                Assert.That(contato.EMail, Is.EqualTo(contatoAlterado.EMail));
                Assert.That(contato.Telefone, Is.EqualTo(contatoAlterado.Telefone));

            }
            finally
            {
                unitOfWork.Rollback();
            }
        }
    }

    [Test]
    public async Task TestExclusaoContato()
    {
        using (IUnitOfWork unitOfWork = GetService<IUnitOfWork>())
        {
            try
            {
                unitOfWork.BeginTransaction();

                Contato contato = new Contato() { Nome = "teste", Telefone = "11 91234-1234", EMail = "teste@teste.com" };
                await unitOfWork.RepositorioContato.CadastrarContato(contato);

                var contatoAlterado = (await unitOfWork.RepositorioContato.BuscaPorDDD("11")).First(c => c.Nome == "teste");

                await unitOfWork.RepositorioContato.ExcluirContato("teste");

                contatoAlterado = (await unitOfWork.RepositorioContato.BuscaPorDDD("11")).FirstOrDefault(c => c.Nome == "teste");

                Assert.IsNull(contatoAlterado);
            }
            finally
            {
                unitOfWork.Rollback();
            }
        }
    }
}
