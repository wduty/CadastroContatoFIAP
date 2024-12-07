using contatos_domain.entidades;

namespace contatos_domain.interfaces.repository;

public interface IRepositorioContato
{
    Task CadastrarContato(ContatoPessoa contato);
    Task<IEnumerable<Contato>> BuscaPorDDD(string ddd);
    Task AlterarContato(ContatoPessoa contato);
    Task ExcluirContato(string nome);
}