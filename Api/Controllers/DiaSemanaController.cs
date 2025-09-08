using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Mediconnet.Api.Controllers;

[ApiController]
[Route("Api/DiaSemana")]
public class DiaSemanaController : ControllerBase
{
    private readonly ITDiaSemanaServices _tDiaSemanaServices;

    public DiaSemanaController(ITDiaSemanaServices tDiaSemanaServices)
    {
        _tDiaSemanaServices = tDiaSemanaServices;
    }

    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetDiaSemanaAsync()
    {
        var diaSemana = await _tDiaSemanaServices.GetDiaSemanaDTOsAsync();
        return Ok(diaSemana);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetDiaSemanaIdAsync(int id)
    {
        var diaSemana = await _tDiaSemanaServices.GetDiaSemanaIdDTOsAsync(id);
        return Ok(diaSemana);
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> PostDiaSemanaAsync([FromBody] TDiaSemanaDTO diaSemanaDTO)
    {
        await _tDiaSemanaServices.CrearAsync(diaSemanaDTO);
        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> PutDiaSemanaAsync([FromBody] TDiaSemanaDTO diaSemanaDTO)
    {
        await _tDiaSemanaServices.ActualizarAsync(diaSemanaDTO.DiaSemanaID, diaSemanaDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> DeleteDiaSemanaAsync(int id)
    {
        await _tDiaSemanaServices.EliminarAsync(id);
        return NoContent();
    }

}