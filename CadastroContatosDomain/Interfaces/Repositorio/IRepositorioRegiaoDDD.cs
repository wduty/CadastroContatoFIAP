using CadastroContatosDomain.Entidades;

namespace CadastroContatosDomain.Interfaces.Repositorio;

public interface IRepositorioRegiaoDDD
{
    RegiaoDDD BuscarPorDDD(string ddd);
}