using CadastroContatosDomain.Entidades;
using CadastroContatosDomain.Interfaces.Repositorio;

namespace CadastroContatosInfrastructure.Repository;

public class RepositorioRegiaoDDD : IRepositorioRegiaoDDD
{
    List<RegiaoDDD> RegioesDDD = new();

    public RepositorioRegiaoDDD()
    {
        foreach (var linha in File.ReadAllLines("RegioesDDD.txt"))
        {
            var campos = linha.Split(';');
            if (campos.Length != 2)
                throw new Exception($"Registro de região de DDD inválida: {linha}");

            RegioesDDD.Add(new RegiaoDDD() { Regiao = campos[0], DDD = campos[1] });
        }
    }

    public RegiaoDDD? BuscarPorDDD(string ddd) => RegioesDDD.FirstOrDefault(r => r.DDD == ddd);
}
