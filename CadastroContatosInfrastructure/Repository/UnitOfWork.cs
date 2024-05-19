using CadastroContatosDomain.Interfaces.Repositorio;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Threading;

namespace CadastroContatosInfrastructure.Repository;

public class UnitOfWork : IUnitOfWork
{
    private AsyncLocal<IDbConnection> conexao = new();

    private AsyncLocal<IDbTransaction> transcao = new();

    private AsyncLocal<IRepositorioContato> repositorioContato = new();

    private RepositorioRegiaoDDD RepositorioRegiaoDDD;

    public UnitOfWork(IConfiguration configuration)
    {
        conexao.Value = new MySqlConnection(configuration.GetConnectionString("Contatos"));
        conexao.Value.Open();

        RepositorioRegiaoDDD = new();
    }

    public void BeginTransaction()
    {
        transcao.Value = conexao.Value?.BeginTransaction();
    }

    public void Commit()
    {
        try
        {
            transcao.Value?.Commit();
        }
        catch
        {
            transcao.Value?.Rollback();
            throw;
        }
        finally
        {
            Dispose();
        }
    }

    public void Rollback()
    {
        transcao.Value?.Rollback();
        Dispose();
    }

    public IRepositorioContato RepositorioContato
    {
        get
        {
            return repositorioContato.Value ??= new RepositorioContato(conexao.Value, RepositorioRegiaoDDD);
        }
    }

    public void Dispose()
    {
        transcao.Value?.Dispose();
        transcao.Value = null;
        conexao.Value?.Dispose();
        conexao.Value = null;
    }
}