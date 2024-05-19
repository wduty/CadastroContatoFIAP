using CadastroContatosDomain.Entidades;

namespace CadastroContatosDomain.Interfaces.Service;

public interface IServiceContato
{
    Task CadastrarContato(ContatoPessoa contato);
    Task<IEnumerable<Contato>> BuscarPorDDD(string ddd);
    Task AlterarContato(ContatoPessoa contato);
    Task ExcluirContato(string nome);
}