using CadastroContatosInfrastructure.Repository;
using Moq;

namespace CadastroContatosTestesUnitarios.Infrastructure;

public class TesteRepositorioRegiao
{
    [Test]
    public void BuscarPorDDD_DeveRetornarRegiaoCorreta()
    {
        var linhasMock = new[]
        {
                "São Paulo;11",
                "Rio de Janeiro;21",
                "Belo Horizonte;31"
            };

        var mockRepositorio = new Mock<RepositorioRegiaoDDD> { CallBase = true };
        mockRepositorio.Setup(r => r.ObterLinhasDoArquivo()).Returns(linhasMock);

        var repositorio = mockRepositorio.Object;
        var regiao = repositorio.BuscarPorDDD("21");

        Assert.That(regiao, Is.Not.Null);
        Assert.That(regiao.Regiao, Is.EqualTo("Rio de Janeiro"));
        Assert.That(regiao.DDD, Is.EqualTo("21"));
    }

    [Test]
    public void BuscarPorDDD_DeveRetornarNullParaDDDInexistente()
    {
        var linhasMock = new[]
        {
                "São Paulo;11",
                "Rio de Janeiro;21",
                "Belo Horizonte;31"
            };

        var mockRepositorio = new Mock<RepositorioRegiaoDDD> { CallBase = true };
        mockRepositorio.Setup(r => r.ObterLinhasDoArquivo()).Returns(linhasMock);

        var repositorio = mockRepositorio.Object;
        var regiao = repositorio.BuscarPorDDD("99");

        Assert.IsNull(regiao);
    }
}