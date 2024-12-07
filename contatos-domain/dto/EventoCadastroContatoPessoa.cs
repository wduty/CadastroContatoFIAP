using contatos_domain.entidades;

namespace contatos_domain.dto;

public class EventoCadastroContatoPessoa : EventoCadastro<ContatoPessoa>
{
    public EventoCadastroContatoPessoa(TipoEventoCadastro tipoEventoCadastro, ContatoPessoa registro) : base(tipoEventoCadastro, registro)
    {
    }
}
