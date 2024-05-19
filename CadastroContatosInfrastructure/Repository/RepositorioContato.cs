using CadastroContatosDomain.Entidades;
using CadastroContatosDomain.Interfaces.Repositorio;
using Dapper;
using System.Data;
using System.Data.Common;

namespace CadastroContatosInfrastructure.Repository;

public class RepositorioContato : IRepositorioContato
{
    private IDbConnection conexao;
    private IRepositorioRegiaoDDD repositorioRegiaoDDD;

    public RepositorioContato(IDbConnection conexao, IRepositorioRegiaoDDD repositorioRegiaoDDD)
    {
        this.conexao = conexao;
        this.repositorioRegiaoDDD = repositorioRegiaoDDD;
    }

    public async Task CadastrarContato(ContatoPessoa contato)
    {
        contato.Validar();

        const string query = @"
            insert into contato (Nome, Telefone, EMail)
            values (@Nome, @Telefone, @EMail)";

        await conexao.ExecuteAsync(query, contato);
    }

    public async Task<IEnumerable<Contato>> BuscaPorDDD(string ddd)
    {
        var regiaoDDD = repositorioRegiaoDDD.BuscarPorDDD(ddd);
        if (regiaoDDD == null)
            throw new ArgumentException("Região DDD não encontrada", nameof(ddd));

        const string query = @"
            select * from contato where telefone like CONCAT(@ddd,' %')";
        
        var contatos = await conexao.QueryAsync<Contato>(query, new { ddd = ddd });

        foreach (var contato in contatos)
            contato.RegiaoDDD = regiaoDDD;

        return contatos;
    }

    public async Task AlterarContato(ContatoPessoa contato)
    {
        contato.Validar();

        const string query = @"
            update contato
            set telefone = @telefone, email = @email
            where Nome = @nome";

        await conexao.ExecuteAsync(query, new { nome = contato.Nome, telefone = contato.Telefone, email = contato.EMail });
    }

    public async Task ExcluirContato(string nome)
    {
        const string query = "delete from contato where nome = @nome";

        await conexao.ExecuteAsync(query, new { nome = nome} );
    }

}
