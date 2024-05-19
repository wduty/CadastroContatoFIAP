namespace CadastroContatosDomain.Entidades;

public class RegiaoDDD
{
    public string DDD { get; set; }
    public string Regiao { get; set; }

    public override string ToString() => $"{DDD} {Regiao}";
}
