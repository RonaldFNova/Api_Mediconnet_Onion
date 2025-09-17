using Api_Mediconnet.Application.Interfaces;
using Api_Mediconnet.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api_Mediconnet.Api.Controllers;

[Route("Api/CodigoVerificacion")]
[ApiController]
public class CodigoVerificacionController : ControllerBase
{
    private readonly ITCodigoVerificacionService _codigoVerificacionService;

    public CodigoVerificacionController(ITCodigoVerificacionService codigoVerificacionService)
    {
        _codigoVerificacionService = codigoVerificacionService;
    }

    [HttpGet]
    [Authorize (Roles = "Administrador")]
    public async Task<ActionResult<IEnumerable<TCodigoVerificacionDTO>>> GetCodigoVerificacionDTOs()
    {
        var codigoVerificacionDTOs = await _codigoVerificacionService.GetCodigoVerificacionDTOsAsync();
        return Ok(codigoVerificacionDTOs);
    }

    [HttpGet("{id}")]
    [Authorize (Roles = "Administrador")]
    public async Task<ActionResult<TCodigoVerificacionDTO>> GetCodigoVerificacionIdDTOs(int id)
    {
        var codigoVerificacionDTO = await _codigoVerificacionService.GetCodigoVerificacionIdDTOsAsync(id);
        return Ok(codigoVerificacionDTO);
    }

    [HttpPost]
    public async Task<ActionResult> CrearCodigoVerificacion([FromBody] TCodigoVerificacionDTO codigoVerificacionDTO)
    {
        await _codigoVerificacionService.CrearAsync(codigoVerificacionDTO);
        return Ok();
    }

    [HttpPut]
    [Authorize (Roles = "Administrador")]
    public async Task<ActionResult> ActualizarCodigoVerificacion([FromBody] TCodigoVerificacionDTO codigoVerificacionDTO)
    {
        await _codigoVerificacionService.ActualizarAsync(codigoVerificacionDTO.CodigoVerificacionID, codigoVerificacionDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize (Roles = "Administrador")]
    public async Task<ActionResult> EliminarCodigoVerificacion(int id)
    {
        await _codigoVerificacionService.EliminarAsync(id);
        return NoContent();
    }
}