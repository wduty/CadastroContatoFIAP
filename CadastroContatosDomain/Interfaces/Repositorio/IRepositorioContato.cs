using CadastroContatosDomain.Entidades;

namespace CadastroContatosDomain.Interfaces.Repositorio;

public interface IRepositorioContato
{
    Task CadastrarContato(ContatoPessoa contato);
    Task<IEnumerable<Contato>> BuscaPorDDD(string ddd);
    Task AlterarContato(ContatoPessoa contato);
    Task ExcluirContato(string nome);
}