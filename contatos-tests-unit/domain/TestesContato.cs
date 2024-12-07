using contatos_domain.entidades;

namespace contatos_tests.domain;

public class TestesContato
{
    [Fact]
    public void TesteNomes()
    {
        var contato = new ContatoPessoa { Nome = "João Silva" };
        Assert.True(contato.NomeValido);

        var contatoSemNome = new ContatoPessoa { Nome = null };
        Assert.False(contatoSemNome.NomeValido);

        var contatoComNomeVazio = new ContatoPessoa { Nome = "" };
        Assert.False(contatoComNomeVazio.NomeValido);
    }

    [Fact]
    public void TesteEmail()
    {
        var contato = new ContatoPessoa { EMail = "email@dominio.com" };
        Assert.True(contato.EMailValido);

        contato = new ContatoPessoa { EMail = "email_invalido" };
        Assert.False(contato.EMailValido);

        var contatoSemEmail = new ContatoPessoa { EMail = null };
        Assert.False(contatoSemEmail.EMailValido);

        var emailsValidos = new[]
        {
            "valid.email@example.com",
            "valid.email+alias@example.com",
            "valid_email@example.com",
            "valid-email@example.com",
            "valid.email@subdomain.example.com",
            "valid-email@example.name",
            "valid.email@example.museum",
            "valid.email@example.co.jp",
            "firstname.lastname@example.com",
            "email@domain-one.com",
            "email@domain.name",
            "email@domain.co.jp",
            "email@domain.com"
        };

        var emailsInvalidos = new[]
        {
            "plainaddress",
            "@missingusername.com",
            "missingat.com",
            "missingdomain@.com",
            "missingat@domain",
            "missingdot@domaincom",
            "two@@domain.com",
            "email@domain..com",
            "email@domain.c",
            "email@domain..com",
            "email@domain.com (Joe Smith)",
            "email@domain",
            "email@111.222.333.44444",
            "email@domain..com",
            "email@domain.c..om",
            "email@..domain.com"
        };

        foreach (var email in emailsValidos)
        {
            contato.EMail = email;
            Assert.True(contato.EMailValido);
        }

        foreach (var email in emailsInvalidos)
        {
            contato.EMail = email;
            Assert.False(contato.EMailValido);
        }
    }

    [Fact]
    public void TesteTelefone()
    {
        var contato = new ContatoPessoa { Telefone = "11 98765-4321" };
        Assert.True(contato.TelefoneValido);

        var contatoComLetras = new ContatoPessoa { Telefone = "11 ABCDE-4321" };
        Assert.False(contatoComLetras.TelefoneValido);

        var contatoSemTelefone = new ContatoPessoa { Telefone = null };
        Assert.False(contatoSemTelefone.TelefoneValido);

        contato = new ContatoPessoa { Telefone = "11 98765-4321" };
        Assert.Equal("11", contato.DDD);

        contato.Telefone = "Teste";
        Assert.False(contato.TelefoneValido);

        contato.Telefone = "10";
        Assert.False(contato.TelefoneValido);

        contato.Telefone = "10-10";
        Assert.False(contato.TelefoneValido);

        contato.Telefone = "999-9999";
        Assert.False(contato.TelefoneValido);

        contato.Telefone = "9999-9999";
        Assert.False(contato.TelefoneValido);

        contato.Telefone = "11 999-9999";
        Assert.False(contato.TelefoneValido);

        contato.Telefone = "11 9999-9999";
        Assert.True(contato.TelefoneValido);
        Assert.Equal("11", contato.DDD);

        contato.Telefone = "11 91234-1234";
        Assert.True(contato.TelefoneValido);
        Assert.Equal("11", contato.DDD);
    }

    [Fact]
    public void TesteContatoValido()
    {
        var contato = new ContatoPessoa
        {
            Nome = "João Silva",
            Telefone = "11 98765-4321",
            EMail = "email@dominio.com"
        };

        Assert.True(contato.ContatoValido);

        var contatoComNomeInvalido = new ContatoPessoa
        {
            Nome = null,
            Telefone = "11 98765-4321",
            EMail = "email@dominio.com"
        };
        Assert.False(contatoComNomeInvalido.ContatoValido);

        var contatoComTelefoneInvalido = new ContatoPessoa
        {
            Nome = "João Silva",
            Telefone = "11 ABCDE-4321",
            EMail = "email@dominio.com"
        };
        Assert.False(contatoComTelefoneInvalido.ContatoValido);

        var contatoComEmailInvalido = new ContatoPessoa
        {
            Nome = "João Silva",
            Telefone = "11 98765-4321",
            EMail = "email_invalido"
        };
        Assert.False(contatoComEmailInvalido.ContatoValido);
    }

    [Fact]
    public void TesteValidar()
    {
        var contatoInvalido = new ContatoPessoa
        {
            Nome = null,
            Telefone = "11 98765-4321",
            EMail = "email@dominio.com"
        };

        var exception = Assert.Throws<ArgumentException>(() => contatoInvalido.Validar());
        Assert.Equal("Nome", exception.ParamName);

        var contatoValido = new ContatoPessoa
        {
            Nome = "João Silva",
            Telefone = "11 98765-4321",
            EMail = "email@dominio.com"
        };

        var e = Record.Exception(() => contatoValido.Validar());
        Assert.Null(e);
    }
}
