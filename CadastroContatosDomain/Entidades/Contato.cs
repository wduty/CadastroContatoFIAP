using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace CadastroContatosDomain.Entidades;

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
    public string DDD => Telefone.Split(' ').First();

    public override string ToString() => $"nome:{Nome} tel:{Telefone} email:{EMail}";

    public void Validar()
    {
        if (!ContatoValido)
        {
            if (!NomeValido)
                throw new ArgumentException("Contato inválido", nameof(Nome));

            if (!TelefoneValido)
                throw new ArgumentException("Contato inválido", nameof(Telefone));

            if (!EMailValido)
                throw new ArgumentException("Contato inválido", nameof(EMail));
        }
    }
}

public class Contato: ContatoPessoa
{
    public RegiaoDDD RegiaoDDD { get; set; }
}
