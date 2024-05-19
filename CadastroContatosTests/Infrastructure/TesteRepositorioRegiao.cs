using CadastroContatosDomain.Interfaces.Repositorio;

namespace CadastroContatosTests.Infrastructure;

public class TesteRepositorioRegiao: TesteBaseDI
{
    IRepositorioRegiaoDDD repositorioRegiaoDDD => GetService<IRepositorioRegiaoDDD>();

    [Test]
    public void TesteCarregamentoRegioesDDD()
    {
        var regiao = repositorioRegiaoDDD.BuscarPorDDD("11");

        Assert.IsNotNull(regiao);
        Assert.That(regiao.Regiao, Is.EqualTo("São Paulo (SP)"));
    }
}
