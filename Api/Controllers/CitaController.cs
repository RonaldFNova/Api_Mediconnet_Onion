using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api_Mediconnet.Api.Controllers;

[Route("Api/Cita")]
[ApiController]
public class CitaController : ControllerBase
{
    private readonly ITCitaService _tCitaService;

    public CitaController(ITCitaService tCitaService)
    {
        _tCitaService = tCitaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCitaAsync()
    {
        var citas = await _tCitaService.GetCitaDTOsAsync();
        return Ok(citas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCitaIdAsync(int id)
    {
        var cita = await _tCitaService.GetCitaIdDTOsAsync(id);
        return Ok(cita);
    }

    [HttpPost]
    public async Task<IActionResult> PostCitaAsync([FromBody] TCitaDTO citaDto)
    {
        await _tCitaService.CrearAsync(citaDto);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> PutCitaAsync(TCitaDTO citaDto)
    {
        await _tCitaService.ActualizarAsync(citaDto.NCitaID, citaDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCitaAsync(int id)
    {
        await _tCitaService.EliminarAsync(id);
        return NoContent();
    }
}