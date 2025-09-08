using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Mediconnet.Api.Controllers;

[ApiController]
[Route("Api/Paciente")]
public class PacienteController : ControllerBase
{
    private readonly ITPacienteService _tPacienteService;

    public PacienteController(ITPacienteService tPacienteService)
    {
        _tPacienteService = tPacienteService;
    }

    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetPacienteAsync()
    {
        var persona = await _tPacienteService.GetPacienteDTOsAsync();
        return Ok(persona);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetPacienteIdAsync(int id)
    {
        var persona = await _tPacienteService.GetPacienteIdDTOsAsync(id);
        return Ok(persona);
    }

    [HttpPost]
    public async Task<IActionResult> PostPacienteAsync([FromBody] TPacienteDTO pacienteDTO)
    {
        await _tPacienteService.CrearAsync(pacienteDTO);
        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> PutPacienteAsync([FromBody] TPacienteDTO pacienteDTO)
    {
        await _tPacienteService.ActualizarAsync(pacienteDTO.PacienteID, pacienteDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> DeletePacienteAsync(int id)
    {
        await _tPacienteService.EliminarAsync(id);
        return NoContent();
    }
}