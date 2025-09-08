using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Api_Mediconnet.Api.Controllers;

[ApiController]
[Route("api/EstadoCita")]
public class EstadoCitaController : ControllerBase
{
    private readonly ITEstadoCitaService _tEstadoCitaService;

    public EstadoCitaController(ITEstadoCitaService tEstadoCitaService)
    {
        _tEstadoCitaService = tEstadoCitaService;
    }
    [Authorize(Roles = "Administrador")]
    [HttpGet]
    public async Task<ActionResult> GetEstadoCitaAsync()
    {
        var estadoCita = await _tEstadoCitaService.GetEstadoCitaDTOsAsync();
        return Ok(estadoCita);
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet("{id}")]
    public async Task<ActionResult<TEstadoCitaDTO>> GetEstadoCitaIdAsync(int id)
    {
        var estadoCita = await _tEstadoCitaService.GetEstadoCitaIdDTOsAsync(id);
        return Ok(estadoCita);
    }

    [HttpPost]
    public async Task<IActionResult> PostEstadoCitaAsync([FromBody] TEstadoCitaDTO estadoCitaDTO)
    {
        await _tEstadoCitaService.CreateAsync(estadoCitaDTO);
        return Ok();
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut]
    public async Task<IActionResult> PutEstadoCitaAsync([FromBody] TEstadoCitaDTO estadoCitaDTO)
    {
        await _tEstadoCitaService.ActualizarAsync(estadoCitaDTO.EstadoCitaID, estadoCitaDTO);
        return NoContent();
    }

    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEstadoCitaAsync(int id)
    {
        await _tEstadoCitaService.EliminarAsync(id);
        return NoContent();
    }
 }