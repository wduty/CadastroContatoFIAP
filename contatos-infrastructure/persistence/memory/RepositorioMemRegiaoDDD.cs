using contatos_domain.entidades;
using contatos_domain.interfaces.repository;

namespace contatos_infrastructure.persistence.memory;

public class RepositorioMemRegiaoDDD : IRepositorioRegiaoDDD
{
    public List<RegiaoDDD> regioesDDD = new()
    {
        new RegiaoDDD() { DDD = "11", Regiao = "São Paulo" },
        new RegiaoDDD() { DDD = "12", Regiao = "São José dos Campos" },
        new RegiaoDDD() { DDD = "13", Regiao = "Santos" },
        new RegiaoDDD() { DDD = "14", Regiao = "Bauru" },
        new RegiaoDDD() { DDD = "15", Regiao = "Sorocaba" },
        new RegiaoDDD() { DDD = "16", Regiao = "Ribeirão Preto" }
    };

    public Task<RegiaoDDD> BuscarPorDDD(string ddd)
        => Task.FromResult(regioesDDD.FirstOrDefault(r => r.DDD == ddd));
}
