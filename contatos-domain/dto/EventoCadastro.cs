namespace contatos_domain.dto;

public enum TipoEventoCadastro
{
    Cadastro,
    Alteracao,
    Exclusao
}

public class EventoCadastro<T>(TipoEventoCadastro tipoEventoCadastro, T registro) where T: class
{
    public TipoEventoCadastro TipoEvento { get; set; } = tipoEventoCadastro;
    public T Dados { get; set; } = registro;
}
