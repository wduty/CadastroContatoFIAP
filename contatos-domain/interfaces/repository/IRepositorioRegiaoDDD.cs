using contatos_domain.entidades;

namespace contatos_domain.interfaces.repository;

public interface IRepositorioRegiaoDDD
{
    Task<RegiaoDDD> BuscarPorDDD(string ddd);
}