using contatos_application.service;
using contatos_domain.interfaces.service;
using contatos_infrastructure.persistence.memory;
using contatos_infrastructure.persistence.rabbitMQ;

namespace contatos_tests.service;

public class TestesServiceEventoContato
{
    [Fact]
    public async Task TesteServiceEventoContato()
    {
        //IServiceContato serviceContato = new ServiceEventoContato(new RepositorioMemContato(new RepositorioMemRegiaoDDD()), );
        //await new ServiceContatoTester().TestServiceContato(serviceContato);
    }
}
