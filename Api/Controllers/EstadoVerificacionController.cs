using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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

    [Authorize(Roles = "Administrador")]
    [HttpGet]
    public async Task<IActionResult> GetEstadoVerificacionAsync()
    {
        var estadoVerificacion = await _tEstadoVerificacionService.GetEstadoVerificacionDTOsAsync();
        return Ok(estadoVerificacion);
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEstadoVerificacionIdAsync(int id)
    {
        var estadoVerificacion = await _tEstadoVerificacionService.GetEstadoVerificacionIdDTOsAsync(id);
        return Ok(estadoVerificacion);
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public async Task<IActionResult> PostEstadoVerificacionIdAsync([FromBody] TEstadoVerificacionDTO estadoVerificacionDTO)
    {
        await _tEstadoVerificacionService.CrearAsync(estadoVerificacionDTO);
        return Ok();
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut]
    public async Task<IActionResult> PutEstadoVerificacionIdAsync([FromBody] TEstadoVerificacionDTO estadoVerificacionDTO)
    {
        await _tEstadoVerificacionService.ActualizarAsync(estadoVerificacionDTO.EstadoVerificacionID, estadoVerificacionDTO);
        return NoContent();
    }

    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEstadoVerificacionIdAsync(int id)
    {
        await _tEstadoVerificacionService.EliminarAsync(id);
        return NoContent();
    }
}
