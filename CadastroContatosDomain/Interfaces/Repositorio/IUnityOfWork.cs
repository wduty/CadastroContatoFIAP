namespace CadastroContatosDomain.Interfaces.Repositorio;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    void Commit();
    void Rollback();
    IRepositorioContato RepositorioContato { get; }
}