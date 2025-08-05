using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api_Mediconnet.Api.Controllers;

[ApiController]
[Route("Api/Usuario")]
public class UsuariosController : ControllerBase
{
    private readonly ITUsuariosService _usuariosService;

    public UsuariosController(ITUsuariosService usuariosService)
    {
        _usuariosService = usuariosService;
    }


    [Authorize(Roles = "Administrador")]
    [HttpGet]
    public async Task<IActionResult> GetUsuarios()
    {
        var usuarios = await _usuariosService.GetUsuariosAsync();
        return Ok(usuarios);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsuario(int id)
    {
        var usuario = await _usuariosService.GetUsuariosIdAsync(id);
        return Ok(usuario);
    }

    [HttpPost]
    public async Task<IActionResult> PostUsuario([FromBody] TUsuarioCreateDTO usuarioCreateDTO)
    {
        string token = await _usuariosService.CrearAsync(usuarioCreateDTO);

        return Ok(new { token });
    }

    [HttpPut]
    public async Task<IActionResult> PutUsuario([FromBody] TUsuarioCreateDTO usuarioCreateDTO)
    {
        await _usuariosService.ActualizarAsync(usuarioCreateDTO.NUsuarioID, usuarioCreateDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        await _usuariosService.EliminarAsync(id);
        return NoContent();
    }
    

}