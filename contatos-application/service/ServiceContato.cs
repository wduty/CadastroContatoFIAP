using contatos_domain.entidades;
using contatos_domain.interfaces.repository;
using contatos_domain.interfaces.service;

namespace contatos_application.service;

public class ServiceContato(IRepositorioContato repositorioContato) : IServiceContato
{
    public async Task CadastrarContato(ContatoPessoa contato)
    {
        contato.Validar();

        await repositorioContato.CadastrarContato(contato);
    }

    public async Task AlterarContato(ContatoPessoa contato)
    {
        contato.Validar();

        await repositorioContato.AlterarContato(contato);
    }

    public async Task<IEnumerable<Contato>> BuscarPorDDD(string ddd)
        => await repositorioContato.BuscaPorDDD(ddd);

    public async Task ExcluirContato(string nome)
        => await repositorioContato.ExcluirContato(nome);
}
