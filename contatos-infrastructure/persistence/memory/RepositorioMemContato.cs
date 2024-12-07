using contatos_domain.entidades;
using contatos_domain.interfaces.repository;
using Force.DeepCloner;

namespace contatos_infrastructure.persistence.memory;

public class RepositorioMemContato(IRepositorioRegiaoDDD repositorioRegiaoDDD) : IRepositorioContato
{
    List<ContatoPessoa> contatos = new();

    public async Task CadastrarContato(ContatoPessoa contato)
    {
        contato.Validar();

        contatos.Add(contato.ShallowClone());

        await Task.CompletedTask;
    }

    public async Task AlterarContato(ContatoPessoa contato)
    {
        contato.Validar();

        var dbContato = contatos.FirstOrDefault(c => c.Nome.Equals(contato.Nome));

        if (dbContato != null)
            contato.ShallowCloneTo(dbContato);

        await Task.CompletedTask;
    }

    public async Task<IEnumerable<Contato>> BuscaPorDDD(string ddd)
    {
        var regiaoDDD = await repositorioRegiaoDDD.BuscarPorDDD(ddd);
        if (regiaoDDD == null)
            throw new ArgumentException("Região DDD não encontrada", nameof(ddd));

        var contatosDDD = contatos.Where(c => c.DDD == ddd).Select(contatoPessoa =>
        {
            Contato contato = new();
            contatoPessoa.ShallowCloneTo(contato);
            contato.RegiaoDDD = regiaoDDD;
            return contato;
        }).ToArray();


        return await Task.FromResult(contatosDDD);
    }

    public async Task ExcluirContato(string nome)
    {
        var dbContato = contatos.FirstOrDefault(c => c.Nome.Equals(nome));

        if (dbContato != null)
            contatos.Remove(dbContato);

        await Task.CompletedTask;
    }
}
