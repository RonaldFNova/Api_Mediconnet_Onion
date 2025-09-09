using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api_Mediconnet.Api.Controllers;

[ApiController]
[Route("Api/EstadoUsuario")]
public class EstadoUsuarioController : ControllerBase
{
    private readonly ITEstadoUsuarioervice _tEstadoUsuarioervice;

    public EstadoUsuarioController(ITEstadoUsuarioervice tEstadoUsuarioervice)
    {
        _tEstadoUsuarioervice = tEstadoUsuarioervice;
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet]
    public async Task<IActionResult> GetEstadoUsuarioAsync()
    {
        var estadoUsuario = await _tEstadoUsuarioervice.GetEstadoUsuarioDTOsAsync();
        return Ok(estadoUsuario);
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEstadoUsuarioIdAsync(int id)
    {
        var estadoUsuario = await _tEstadoUsuarioervice.GetEstadoUsuarioIdDTOsAsync(id);
        return Ok(estadoUsuario);
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public async Task<IActionResult> PostEstadoUsuario([FromBody] TEstadoUsuarioDTO estadoUsuarioDTO)
    {
        await _tEstadoUsuarioervice.CrearAsync(estadoUsuarioDTO);
        return Ok();
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut]
    public async Task<IActionResult> PutEstadoUsuario([FromBody] TEstadoUsuarioDTO estadoUsuarioDTO)
    {
        await _tEstadoUsuarioervice.ActualizarAsync(estadoUsuarioDTO.EstadoUsuarioID, estadoUsuarioDTO);
        return NoContent();
    }

    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEstadoUsuario( int id)
    {
        await _tEstadoUsuarioervice.EliminarAsync(id);
        return NoContent();
    }
}