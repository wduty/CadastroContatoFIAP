using contatos_domain.dto;
using contatos_domain.entidades;
using contatos_domain.interfaces.events;
using contatos_domain.interfaces.repository;

namespace contatos_infrastructure.events;

public class EventoCadastroContatoProcess
{
    private readonly IRepositorioContato repositorioContato;
    private readonly IEventConsumer<EventoCadastroContatoPessoa> eventConsumer;

    public EventoCadastroContatoProcess(IRepositorioContato repositorioContato, IEventConsumer<EventoCadastroContatoPessoa> eventConsumer)
    {
        this.repositorioContato = repositorioContato;
        this.eventConsumer = eventConsumer;

        eventConsumer.ConsumeAsync(EventCadastro).Wait();
    }

    private async Task EventCadastro(EventoCadastroContatoPessoa eventoCadastro) => 
        await ProcessarEventoCadastro(eventoCadastro);

    public async Task ProcessarEventoCadastro(EventoCadastroContatoPessoa eventoCadastro)
    {
        if (eventoCadastro?.Dados == null)
            throw new ArgumentException("Dados do evento não informados");

        switch (eventoCadastro.TipoEvento)
        {
            case TipoEventoCadastro.Cadastro:
                await repositorioContato.CadastrarContato(eventoCadastro.Dados);
                break;

            case TipoEventoCadastro.Alteracao:
                await repositorioContato.AlterarContato(eventoCadastro.Dados);
                break;

            case TipoEventoCadastro.Exclusao:
                await repositorioContato.ExcluirContato(eventoCadastro.Dados.Nome);
                break;
        }
    }
}
