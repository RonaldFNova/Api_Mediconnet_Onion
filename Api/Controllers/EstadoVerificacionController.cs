
using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api_Mediconnet.Api.Controllers;

[ApiController]
[Route("Api/EstadoVerificacion")]
public class EstadoVerificacionController : ControllerBase
{
    private readonly ITEstadoVerificacionService _tEstadoVerificacionService;

    public EstadoVerificacionController(ITEstadoVerificacionService tEstadoVerificacionService)
    {
        _tEstadoVerificacionService = tEstadoVerificacionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetEstadoVerificacionAsync()
    {
        var estadoVerificacion = await _tEstadoVerificacionService.GetEstadoVerificacionDTOsAsync();
        return Ok(estadoVerificacion);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEstadoVerificacionIdAsync(int id)
    {
        var estadoVerificacion = await _tEstadoVerificacionService.GetEstadoVerificacionIdDTOsAsync(id);
        return Ok(estadoVerificacion);
    }

    [HttpPost]
    public async Task<IActionResult> PostEstadoVerificacionIdAsync([FromBody] TEstadoVerificacionDTO estadoVerificacionDTO)
    {
        await _tEstadoVerificacionService.CrearAsync(estadoVerificacionDTO);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> PutEstadoVerificacionIdAsync([FromBody] TEstadoVerificacionDTO estadoVerificacionDTO)
    {
        await _tEstadoVerificacionService.ActualizarAsync(estadoVerificacionDTO.NEstadoVerificacionID, estadoVerificacionDTO);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEstadoVerificacionIdAsync(int id)
    {
        await _tEstadoVerificacionService.EliminarAsync(id);
        return NoContent();
    }
}
