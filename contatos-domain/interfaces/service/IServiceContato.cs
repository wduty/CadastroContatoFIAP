using contatos_domain.entidades;

namespace contatos_domain.interfaces.service;

public interface IServiceContato
{
    Task CadastrarContato(ContatoPessoa contato);
    Task<IEnumerable<Contato>> BuscarPorDDD(string ddd);
    Task AlterarContato(ContatoPessoa contato);
    Task ExcluirContato(string nome);
}