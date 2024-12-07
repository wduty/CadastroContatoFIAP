using contatos_domain.dto;
using contatos_domain.entidades;
using contatos_domain.interfaces.events;
using contatos_domain.interfaces.repository;
using contatos_domain.interfaces.service;

namespace contatos_infrastructure.persistence.rabbitMQ;

public class ServiceEventoContato(IRepositorioContato repositorioContato, IEventProducer<EventoCadastroContatoPessoa> eventProducer) : IServiceContato
{
    public async Task CadastrarContato(ContatoPessoa contato)
    {
        contato.Validar();

        await eventProducer.PublishAsync(new EventoCadastroContatoPessoa( TipoEventoCadastro.Cadastro, contato));
    }

    public async Task AlterarContato(ContatoPessoa contato)
    {
        contato.Validar();

        await eventProducer.PublishAsync(new EventoCadastroContatoPessoa(TipoEventoCadastro.Alteracao, contato));
    }

    public async Task ExcluirContato(string nome) =>
        await eventProducer.PublishAsync(new EventoCadastroContatoPessoa(TipoEventoCadastro.Exclusao, new ContatoPessoa() { Nome = nome }));

    public async Task<IEnumerable<Contato>> BuscarPorDDD(string ddd)
        => await repositorioContato.BuscaPorDDD(ddd);
}

