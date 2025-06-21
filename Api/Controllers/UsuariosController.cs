using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
        await _usuariosService.CrearAsync(usuarioCreateDTO);
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> PutUsuario([FromBody] TUsuarioCreateDTO usuarioCreateDTO)
    {
        await _usuariosService.ActualizarAsync(usuarioCreateDTO.NUsuarioID, usuarioCreateDTO);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUsuario([FromBody] int id)
    {
        await _usuariosService.EliminarAsync(id);
        return NoContent();
    }
    

}