using Microsoft.AspNetCore.Mvc;
using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Api_Mediconne.Api.Controllers;

public class ProfesionalController : ControllerBase
{
    private readonly ITProfesionalService _tProfesionalService;

    public ProfesionalController(ITProfesionalService tProfesionalService)
    {
        _tProfesionalService = tProfesionalService;
    }

    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetProfesionalAsync()
    {
        var profesional = await _tProfesionalService.GetProfesionalDTOsAsync();
        return Ok(profesional);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetProfesionalIdAsync([FromBody] int id)
    {
        var profesional = await _tProfesionalService.GetProfesionalIdDTOsAsync(id);
        return Ok(profesional);
    }

    [HttpPost]
    public async Task<IActionResult> PostProfesionalAsync([FromBody] TProfesionalDTO profesionalDTO)
    {
        await _tProfesionalService.CrearAsync(profesionalDTO);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> PutProfesionalAsync([FromBody] TProfesionalDTO profesionalDTO)
    {
        await _tProfesionalService.ActualizarAsync(profesionalDTO.ProfesionalID, profesionalDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfesionalAsync(int id)
    {
        await _tProfesionalService.EliminarAsync(id);
        return NoContent();
    }
}
