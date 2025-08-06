using Microsoft.AspNetCore.Mvc;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Api_Mediconnet.Api.Controllers;

[ApiController]
[Route("Api/TipoIdentificacion")]
public class TipoIdentificacionController : ControllerBase
{
    private readonly ITipoIdentificacionService _tipoIdentificacionService;

    public TipoIdentificacionController(ITipoIdentificacionService tipoIdentificacionService)
    {
        _tipoIdentificacionService = tipoIdentificacionService;
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet]
    public async Task<IActionResult> GetTipoIdentificacionAsync()
    {
        var tipoIdentificacion = await _tipoIdentificacionService.GetTipoIdentificacionDTOsAsync();
        return Ok(tipoIdentificacion);
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTipoIdentificacionIdAsync(int id)
    {
        var tipoIdentificacion = await _tipoIdentificacionService.GetTipoIdentificacionIdDTOsAsync(id);
        return Ok(tipoIdentificacion);
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public async Task<IActionResult> PostTipoIdentificacionAsync([FromBody] TTipoIdentificacionDTO tipoIdentificacionDTO)
    {
        await _tipoIdentificacionService.CrearAsync(tipoIdentificacionDTO);
        return Ok();
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut]
    public async Task<IActionResult> PutTipoIdentificacionAsync([FromBody] TTipoIdentificacionDTO tipoIdentificacionDTO)
    {
        await _tipoIdentificacionService.ActualizarAsync(tipoIdentificacionDTO.NTipoIdentificacionID, tipoIdentificacionDTO);
        return NoContent();
    }

    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTipoIdentificacionAsync(int id)
    {
        await _tipoIdentificacionService.EliminarAsync(id);
        return NoContent();
    }
}