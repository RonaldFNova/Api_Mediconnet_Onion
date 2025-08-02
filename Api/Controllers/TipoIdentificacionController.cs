using Microsoft.AspNetCore.Mvc;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Application.DTOs;
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

    [HttpGet]
    public async Task<IActionResult> GetTipoIdentificacionAsync()
    {
        var tipoIdentificacion = await _tipoIdentificacionService.GetTipoIdentificacionDTOsAsync();
        return Ok(tipoIdentificacion);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTipoIdentificacionIdAsync(int id)
    {
        var tipoIdentificacion = await _tipoIdentificacionService.GetTipoIdentificacionIdDTOsAsync(id);
        return Ok(tipoIdentificacion);
    }
    [HttpPost]
    public async Task<IActionResult> PostTipoIdentificacionAsync([FromBody] TTipoIdentificacionDTO tipoIdentificacionDTO)
    {
        await _tipoIdentificacionService.CrearAsync(tipoIdentificacionDTO);
        return Ok();
    }
    [HttpPut]
    public async Task<IActionResult> PutTipoIdentificacionAsync([FromBody] TTipoIdentificacionDTO tipoIdentificacionDTO)
    {
        await _tipoIdentificacionService.ActualizarAsync(tipoIdentificacionDTO.NTipoIdentificacionID, tipoIdentificacionDTO);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTipoIdentificacionAsync(int id)
    {
        await _tipoIdentificacionService.EliminarAsync(id);
        return NoContent();
    }
}