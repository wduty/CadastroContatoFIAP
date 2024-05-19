using CadastroContatosDomain.Entidades;
using CadastroContatosDomain.Interfaces.Repositorio;
using CadastroContatosDomain.Interfaces.Service;
using Microsoft.Extensions.DependencyInjection;

namespace CadastroContatosApplication.Service;

public class ServiceContatos : IServiceContato
{
    private readonly IServiceProvider serviceProvider;

    public ServiceContatos(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task CadastrarContato(ContatoPessoa contato)
    {
        using (IUnitOfWork unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>())
        {
            unitOfWork.BeginTransaction();

            try
            {
                await unitOfWork.RepositorioContato.CadastrarContato(contato);
            }
            finally
            {
                unitOfWork.Commit();
            }
        }
    }

    public async Task<IEnumerable<Contato>> BuscarPorDDD(string ddd)
    {
        using (IUnitOfWork unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>())
            return await unitOfWork.RepositorioContato.BuscaPorDDD(ddd);
    }

    public async Task AlterarContato(ContatoPessoa contato)
    {
        using (IUnitOfWork unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>())
        {
            unitOfWork.BeginTransaction();

            try
            {
                await unitOfWork.RepositorioContato.AlterarContato(contato);
            }
            finally
            {
                unitOfWork.Commit();
            }
        }
    }

    public async Task ExcluirContato(string nome)
    {
        using (IUnitOfWork unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>())
        {
            unitOfWork.BeginTransaction();

            try
            {
                await unitOfWork.RepositorioContato.ExcluirContato(nome);
            }
            finally
            {
                unitOfWork.Commit();
            }
        }
    }
}
