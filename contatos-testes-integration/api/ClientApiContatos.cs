using contatos_domain.dto;
using contatos_domain.entidades;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace contatos_testes_integration.api;

public class ClientApiContatos
{
    public HttpResponseMessage Response { get; private set; }
    public ApiError ApiError { get; private set; }

    public HttpStatusCode StatusCode => Response?.StatusCode ?? HttpStatusCode.OK;

    private readonly HttpClient HttpClient;

    public ClientApiContatos(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    void Reset()
    {
        Response = null;
        ApiError = new("");
    }

    StringContent SerializeObject(object data) => new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

    public async Task Cadastrar(ContatoPessoa contatoPessoa)
    {
        Reset();

        Response = await HttpClient.PostAsync($"api/contatos/cadastrar", SerializeObject(contatoPessoa));

        if ((int)StatusCode > 299)
            ApiError = await Response.Content.ReadFromJsonAsync<ApiError>();
    }

    public async Task<Contato[]> BuscarPorDDD(string ddd)
    {
        Reset();

        Response = await HttpClient.GetAsync($"api/contatos/buscar-por-ddd/{ddd}");

        Contato[] contatos = [];

        if ((int)StatusCode > 299)
            ApiError = await Response.Content.ReadFromJsonAsync<ApiError>();
        else
            contatos = await Response.Content.ReadFromJsonAsync<Contato[]>();

        return contatos;
    }

    public async Task Alterar(ContatoPessoa contatoPessoa)
    {
        Reset();

        Response = await HttpClient.PatchAsync($"api/contatos/alterar", SerializeObject(contatoPessoa));

        if ((int)StatusCode > 299)
            ApiError = await Response.Content.ReadFromJsonAsync<ApiError>(); ;
    }

    public async Task Excluir(string nome)
    {
        Reset();

        Response = await HttpClient.DeleteAsync($"api/contatos/excluir/{nome}");

        if ((int)StatusCode > 299)
            ApiError = await Response.Content.ReadFromJsonAsync<ApiError>(); ;
    }
}
