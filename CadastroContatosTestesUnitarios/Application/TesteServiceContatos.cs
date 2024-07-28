using CadastroContatosApplication.Service;
using CadastroContatosDomain.Entidades;
using CadastroContatosDomain.Interfaces.Repositorio;
using Moq;

namespace CadastroContatosTestesUnitarios.Application
{
    [TestFixture]
    public class ServiceContatosTests
    {
        private Mock<IServiceProvider> mockServiceProvider;
        private Mock<IUnitOfWork> mockUnitOfWork;
        private Mock<IRepositorioContato> mockRepositorioContato;
        private ServiceContatos serviceContatos;

        [SetUp]
        public void Setup()
        {
            mockServiceProvider = new Mock<IServiceProvider>();
            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockRepositorioContato = new Mock<IRepositorioContato>();

            mockUnitOfWork.Setup(u => u.RepositorioContato).Returns(mockRepositorioContato.Object);

            mockServiceProvider.Setup(sp => sp.GetService(typeof(IUnitOfWork))).Returns(mockUnitOfWork.Object);

            serviceContatos = new ServiceContatos(mockServiceProvider.Object);
        }

        [Test]
        public async Task CadastrarContato_DeveChamarRepositorioCadastrarContato()
        {
            // Arrange
            var contato = new ContatoPessoa { Nome = "Teste", Telefone = "11 91234-5678", EMail = "teste@teste.com" };

            // Act
            await serviceContatos.CadastrarContato(contato);

            // Assert
            mockRepositorioContato.Verify(r => r.CadastrarContato(contato), Times.Once);
            mockUnitOfWork.Verify(u => u.BeginTransaction(), Times.Once);
            mockUnitOfWork.Verify(u => u.Commit(), Times.Once);
        }

        [Test]
        public async Task BuscarPorDDD_DeveRetornarContatos()
        {
            // Arrange
            var ddd = "11";
            var contatosMock = new List<Contato>
            {
                new Contato { Nome = "Teste", Telefone = "11 91234-5678", EMail = "teste@teste.com" }
            };

            mockRepositorioContato.Setup(r => r.BuscaPorDDD(ddd)).ReturnsAsync(contatosMock);

            // Act
            var result = await serviceContatos.BuscarPorDDD(ddd);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Telefone.Substring(0, 2), Is.EqualTo("11"));
            mockRepositorioContato.Verify(r => r.BuscaPorDDD(ddd), Times.Once);
        }

        [Test]
        public async Task AlterarContato_DeveChamarRepositorioAlterarContato()
        {
            // Arrange
            var contato = new ContatoPessoa { Nome = "Teste", Telefone = "11 91234-5678", EMail = "teste@teste.com" };

            // Act
            await serviceContatos.AlterarContato(contato);

            // Assert
            mockRepositorioContato.Verify(r => r.AlterarContato(contato), Times.Once);
            mockUnitOfWork.Verify(u => u.BeginTransaction(), Times.Once);
            mockUnitOfWork.Verify(u => u.Commit(), Times.Once);
        }

        [Test]
        public async Task ExcluirContato_DeveChamarRepositorioExcluirContato()
        {
            // Arrange
            var nome = "Teste";

            // Act
            await serviceContatos.ExcluirContato(nome);

            // Assert
            mockRepositorioContato.Verify(r => r.ExcluirContato(nome), Times.Once);
            mockUnitOfWork.Verify(u => u.BeginTransaction(), Times.Once);
            mockUnitOfWork.Verify(u => u.Commit(), Times.Once);
        }
    }
}
