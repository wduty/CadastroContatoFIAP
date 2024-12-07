using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace contatos_domain.entidades;

public class ContatoPessoa
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string EMail { get; set; }

    [JsonIgnore]
    public bool NomeValido => !string.IsNullOrWhiteSpace(Nome);

    [JsonIgnore]
    public bool EMailValido
    {
        get
        {
            if (string.IsNullOrWhiteSpace(EMail))
                return false;

            return Regex.IsMatch(EMail, @"^(?!.*\.\.)[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        }
    }

    [JsonIgnore]
    public bool TelefoneValido
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Telefone))
                return false;

            if (Telefone.Any(c => char.IsLetter(c)))
                return false;

            return Regex.IsMatch(Telefone, @"^\d{2}\s?\d{4,5}-\d{4}$");
        }
    }

    [JsonIgnore]
    public bool ContatoValido => NomeValido && EMailValido && TelefoneValido;

    [JsonIgnore]
    public string DDD => Telefone?.Split(' ').First();

    public override string ToString() => $"nome:{Nome} tel:{Telefone} email:{EMail}";
}

public static class ValidarContato
{
    public static void Validar(this ContatoPessoa contatoPessoa)
    {
        if (contatoPessoa == null)
            throw new Exception("Contato nulo");

        if (!contatoPessoa.ContatoValido)
        {
            if (!contatoPessoa.NomeValido)
                throw new ArgumentException("Contato inválido", nameof(contatoPessoa.Nome));

            if (!contatoPessoa.TelefoneValido)
                throw new ArgumentException("Contato inválido", nameof(contatoPessoa.Telefone));

            if (!contatoPessoa.EMailValido)
                throw new ArgumentException("Contato inválido", nameof(contatoPessoa.EMail));
        }
    }
}

public class Contato : ContatoPessoa
{
    public RegiaoDDD RegiaoDDD { get; set; }
}
