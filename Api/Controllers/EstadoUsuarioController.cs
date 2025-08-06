using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api_Mediconnet.Api.Controllers;

[ApiController]
[Route("Api/EstadoUsuario")]
public class EstadoUsuarioController : ControllerBase
{
    private readonly ITEstadoUsuarioService _tEstadoUsuarioService;

    public EstadoUsuarioController(ITEstadoUsuarioService tEstadoUsuarioService)
    {
        _tEstadoUsuarioService = tEstadoUsuarioService;
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet]
    public async Task<IActionResult> GetEstadoUsuariosAsync()
    {
        var estadoUsuario = await _tEstadoUsuarioService.GetEstadoUsuarioDTOsAsync();
        return Ok(estadoUsuario);
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEstadoUsuariosIdAsync(int id)
    {
        var estadoUsuario = await _tEstadoUsuarioService.GetEstadoUsuarioIdDTOsAsync(id);
        return Ok(estadoUsuario);
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public async Task<IActionResult> PostEstadoUsuario([FromBody] TEstadoUsuarioDTO estadoUsuarioDTO)
    {
        await _tEstadoUsuarioService.CrearAsync(estadoUsuarioDTO);
        return Ok();
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut]
    public async Task<IActionResult> PutEstadoUsuario([FromBody] TEstadoUsuarioDTO estadoUsuarioDTO)
    {
        await _tEstadoUsuarioService.ActualizarAsync(estadoUsuarioDTO.NEstadoUsuarioID, estadoUsuarioDTO);
        return NoContent();
    }

    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEstadoUsuario( int id)
    {
        await _tEstadoUsuarioService.EliminarAsync(id);
        return NoContent();
    }
}