using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api_Mediconnet.Api.Controllers;

[ApiController]
[Route("Api/GrupoSanguineo")]
public class GrupoSanguineoController : ControllerBase
{
    private readonly ITGrupoSanguineoService _tGrupoSanguineoService;

    public GrupoSanguineoController(ITGrupoSanguineoService tGrupoSanguineoService)
    {
        _tGrupoSanguineoService = tGrupoSanguineoService;
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet]
    public async Task<IActionResult> GetEstadoVerificacionAsync()
    {
        var grupoSanguineo = await _tGrupoSanguineoService.GetGrupoSanguineoDTOsAsync();
        return Ok(grupoSanguineo);
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEstadoVerificacionIdAsync(int id)
    {
        var grupoSanguineo = await _tGrupoSanguineoService.GetGrupoSanguineoIdDTOsAsync(id);
        return Ok(grupoSanguineo);
    }

    [HttpPost]
    public async Task<IActionResult> PostEstadoVerificacionAsync([FromBody] TGrupoSanguineoDTO grupoSanguineoDTO)
    {
        await _tGrupoSanguineoService.CrearAsync(grupoSanguineoDTO);
        return Ok();
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut]
    public async Task<IActionResult> PutEstadoVerificacionAsync([FromBody] TGrupoSanguineoDTO grupoSanguineoDTO)
    {
        await _tGrupoSanguineoService.ActualizarAsync(grupoSanguineoDTO.GrupoSanguineoID, grupoSanguineoDTO);
        return NoContent();
    }

    [Authorize(Roles = "Administrador")]
    [HttpDelete]
    public async Task<IActionResult> DeleteEstadoVerificacionAsync([FromBody] int id)
    {
        await _tGrupoSanguineoService.EliminarAsync(id);
        return NoContent();
    }

}