using contatos_domain.dto;
using contatos_domain.entidades;
using contatos_domain.interfaces.service;
using Microsoft.AspNetCore.Mvc;

namespace contatos_api.controllers;

[ApiController]
[Route("api/contatos")]
public class ContatosController : ControllerBase
{
    private readonly IServiceContato serviceContato;

    public ContatosController(IServiceContato serviceContato)
    {
        this.serviceContato = serviceContato;
    }

    [HttpPost]
    [Route("cadastrar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiError))]
    public async Task<IActionResult> CadastrarContato([FromBody] ContatoPessoa contato)
    {
        await serviceContato.CadastrarContato(contato);

        return Created();
    }

    [HttpGet]
    [Route("buscar-por-ddd/{ddd}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Contato[]))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiError))]
    public async Task<IActionResult> BuscarPorDDD(string ddd)
    {
        var contatos = await serviceContato.BuscarPorDDD(ddd);

        return Ok(contatos);
    }

    [HttpPatch]
    [Route("alterar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiError))]
    public async Task<IActionResult> Alterar(ContatoPessoa contato)
    {
        await serviceContato.AlterarContato(contato);

        return Ok();
    }

    [HttpDelete]
    [Route("excluir/{nome}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiError))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiError))]
    public async Task<IActionResult> Excluir(string nome)
    {
        await serviceContato.ExcluirContato(nome);

        return Ok();
    }
}
