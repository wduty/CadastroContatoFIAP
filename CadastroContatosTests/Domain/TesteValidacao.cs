using CadastroContatosDomain.Entidades;

namespace CadastroContatosTests.Domain;

public class TesteValidacao
{
    [Test]
    public void TesteValidacaoContatoNome()
    {
        Contato contato = new();

        contato.Nome = null;
        Assert.IsFalse(contato.NomeValido);

        contato.Nome = string.Empty;
        Assert.IsFalse(contato.NomeValido);

        contato.Nome = " ";
        Assert.IsFalse(contato.NomeValido);

        contato.Nome = "Teste";
        Assert.IsTrue(contato.NomeValido);
    }

    [Test]
    public void TesteValidacaoContatoTelefone()
    {
        Contato contato = new();

        contato.Telefone = "Teste";
        Assert.IsFalse(contato.TelefoneValido);

        contato.Telefone = "10";
        Assert.IsFalse(contato.TelefoneValido);

        contato.Telefone = "10-10";
        Assert.IsFalse(contato.TelefoneValido);

        contato.Telefone = "999-9999";
        Assert.IsFalse(contato.TelefoneValido);

        contato.Telefone = "9999-9999";
        Assert.IsFalse(contato.TelefoneValido);

        contato.Telefone = "11 999-9999";
        Assert.IsFalse(contato.TelefoneValido);

        contato.Telefone = "11 9999-9999";
        Assert.IsTrue(contato.TelefoneValido);
        Assert.That(contato.DDD, Is.EqualTo("11"));

        contato.Telefone = "11 91234-1234";
        Assert.IsTrue(contato.TelefoneValido);
        Assert.That(contato.DDD, Is.EqualTo("11"));
    }

    [Test]
    public void TesteValidacaoContatoEmail()
    {
        Contato contato = new();

        // Arrange
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
}