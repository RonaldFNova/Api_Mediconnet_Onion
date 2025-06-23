using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    public async Task<IActionResult> GetRolAsync()
    {
        var rol = await _rolService.GetRolDTOsAsync();
        return Ok(rol);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRolIdAsync(int id)
    {
        var rol = await _rolService.GetRolIdAsync(id);
        return Ok(rol);
    }

    [HttpPost]
    public async Task<IActionResult> PostRolAsync([FromBody] TRolDTO rolDTO)
    {
        await _rolService.CrearAsync(rolDTO);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> PutRolAsync([FromBody] TRolDTO rolDTO)
    {
        await _rolService.ActualizarAsync(rolDTO.NRolID, rolDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRolAsync(int id)
    {
        await _rolService.EliminarAsync(id);
        return NoContent();
    }

}