using contatos_domain.entidades;
using contatos_domain.interfaces.repository;

namespace contatos_infrastructure.persistence.text
{
    public class RepositorioRegiaoDDD : IRepositorioRegiaoDDD
    {
        List<RegiaoDDD> RegioesDDD = new();

        public virtual string[] ObterLinhasDoArquivo() => File.ReadAllLines("RegioesDDD.txt");

        public RepositorioRegiaoDDD()
        {
            foreach (var linha in ObterLinhasDoArquivo())
            {
                var campos = linha.Split(';');
                if (campos.Length != 2)
                    throw new Exception($"Registro de região de DDD inválida: {linha}");

                RegioesDDD.Add(new RegiaoDDD() { Regiao = campos[0], DDD = campos[1] });
            }
        }

        public async Task<RegiaoDDD> BuscarPorDDD(string ddd) => await Task.FromResult(RegioesDDD.FirstOrDefault(r => r.DDD == ddd));
    }
}