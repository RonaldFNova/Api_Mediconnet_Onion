using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Mediconnet.Api.Controllers;

[ApiController]
[Route("Api/Persona")]
public class PersonaController : ControllerBase
{
    private readonly ITPersonaService _tPersonaService;

    public PersonaController(ITPersonaService tPersonaService)
    {
        _tPersonaService = tPersonaService;
    }

    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetPersonaAsync()
    {
        var persona = await _tPersonaService.GetPersonaDTOsAsync();
        return Ok(persona);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetPersonaIdAsync([FromBody] int id)
    {
        var persona = await _tPersonaService.GetPersonaIdDTOsAsync(id);
        return Ok(persona);
    }

    [HttpPost]
    public async Task<IActionResult> PostPersonaAsync([FromBody] TPersonaDTO personaDTO)
    {
        await _tPersonaService.CrearAsync(personaDTO);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> PutPersonaAsync([FromBody] TPersonaDTO personaDTO)
    {
        await _tPersonaService.ActualizarAsync(personaDTO.PersonaID, personaDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePersonaAsync(int id)
    {
        await _tPersonaService.EliminarAsync(id);
        return NoContent();
    }

}