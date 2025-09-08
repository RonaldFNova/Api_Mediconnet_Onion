using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api_Mediconnet.Api.Controllers;

[ApiController]
[Route("Api/Rol")]
public class RolController : ControllerBase
{
    private readonly ITRolService _rolService;
    public RolController(ITRolService rolService)
    {
        _rolService = rolService;
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet]
    public async Task<IActionResult> GetRolAsync()
    {
        var rol = await _rolService.GetRolDTOsAsync();
        return Ok(rol);
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRolIdAsync(int id)
    {
        var rol = await _rolService.GetRolIdAsync(id);
        return Ok(rol);
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public async Task<IActionResult> PostRolAsync([FromBody] TRolDTO rolDTO)
    {
        await _rolService.CrearAsync(rolDTO);
        return Ok();
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut]
    public async Task<IActionResult> PutRolAsync([FromBody] TRolDTO rolDTO)
    {
        await _rolService.ActualizarAsync(rolDTO.RolID, rolDTO);
        return NoContent();
    }

    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRolAsync(int id)
    {
        await _rolService.EliminarAsync(id);
        return NoContent();
    }

}