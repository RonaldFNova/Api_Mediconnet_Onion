using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Mediconnet.Api.Controllers;

[ApiController]
[Route("/Api/Especialidad")]
public class EspecialidadController : ControllerBase
{
    private readonly ITEspecialidadService _tEspecialidadService;

    public EspecialidadController(ITEspecialidadService tEspecialidadService)
    {
        _tEspecialidadService = tEspecialidadService;
    }


    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetEspecialidadAsync()
    {
        var especialidad = await _tEspecialidadService.GetEspecialidadDTOsAsync();
        return Ok(especialidad);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetEspecialidadIdAsync(int id)
    {
        var especialidad = await _tEspecialidadService.GetEspecialidadIdDTOsAsync(id);
        return Ok(especialidad);
    }

    [HttpPost]
    public async Task<IActionResult> PostEspecialidadAsync([FromBody] TEspecialidadDTO especialidadDTO)
    {
        await _tEspecialidadService.CrearAsync(especialidadDTO);
        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> PutEspecialidadAsync([FromBody] TEspecialidadDTO especialidadDTO)
    {
        await _tEspecialidadService.ActualizarAsync(especialidadDTO.EspecialidadID, especialidadDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> DeleteEspecialidadAsync(int id)
    {
        await _tEspecialidadService.EliminarAsync(id);
        return NoContent();
    }
}