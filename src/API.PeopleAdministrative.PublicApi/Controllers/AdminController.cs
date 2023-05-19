using API.PeopleAdministrative.Application.Interfaces;
using API.PeopleAdministrative.Application.Requests;
using API.PeopleAdministrative.Domain.Entities;
using API.PeopleAdministrative.Shared.Extensions;
using API.PeopleAdministrative.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;

namespace API.PeopleAdministrative.PublicApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminService _service;

    public AdminController(IAdminService service) => _service = service;

    /// <summary>
    /// Cria um cadastro.
    /// </summary>
    /// <param name="request">Informações do cadastro</param>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] CreatePersonRequest request)
        => (await _service.CreateAsync(request)).ToActionResult();

    /// <summary>
    /// Consulta o cadastro.
    /// </summary>
    /// <param name="cpf">Cpf da consulta</param>
    [HttpGet]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<Person>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByCpf([FromQuery] int cpf)
        => (await _service.GetAsync(cpf)).ToActionResult();

    /// <summary>
    /// Atualiza as informações cadastrais.
    /// </summary>
    /// <param name="request">Informações a serem atualizadas</param>
    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put([FromBody] UpdatePersonRequest request)
        => (await _service.UpdateAsync(request)).ToActionResult();

    /// <summary>
    /// Exclui uma pessoa.
    /// </summary>
    /// <param name="cpf">Cpf a ser excluido</param>
    [HttpDelete("{cpf}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromRoute] int cpf)
        => (await _service.DeleteAsync(cpf)).ToActionResult();
}