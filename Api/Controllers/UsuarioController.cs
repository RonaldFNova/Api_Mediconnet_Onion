using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api_Mediconnet.Api.Controllers;

[ApiController]
[Route("Api/Usuario")]
public class UsuarioController : ControllerBase
{
    private readonly ITUsuarioService _UsuarioService;

    public UsuarioController(ITUsuarioService UsuarioService)
    {
        _UsuarioService = UsuarioService;
    }


    [Authorize(Roles = "Administrador")]
    [HttpGet]
    public async Task<IActionResult> GetUsuario()
    {
        var Usuario = await _UsuarioService.GetUsuarioAsync();
        return Ok(Usuario);
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsuario(int id)
    {
        var usuario = await _UsuarioService.GetUsuarioIdAsync(id);
        return Ok(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> PostUsuario([FromBody] TUsuarioCreateDTO usuarioCreateDTO)
    {
        await _UsuarioService.CrearAsync(usuarioCreateDTO);
        return Ok();
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut]
    public async Task<IActionResult> PutUsuario([FromBody] TUsuarioCreateDTO usuarioCreateDTO)
    {
        await _UsuarioService.ActualizarAsync(usuarioCreateDTO.UsuarioID, usuarioCreateDTO);
        return NoContent();
    }

    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        await _UsuarioService.EliminarAsync(id);
        return NoContent();
    }
    

}