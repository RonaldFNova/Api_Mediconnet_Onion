using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Mediconnet.Api.Controllers;

[ApiController]
[Route("Api/Login")]
public class LoginsController : ControllerBase
{
    private readonly ITLoginsService _tLoginsService;

    public LoginsController(ITLoginsService tLoginsService)
    {
        _tLoginsService = tLoginsService;
    }

    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetLoginsAsync()
    {
        var Logins = await _tLoginsService.GetLoginsDTOsAsync();
        return Ok(Logins);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetLoginsIdAsync([FromBody] int id)
    {
        var logins = await _tLoginsService.GetLoginsIdDTOsAsync(id);
        return Ok(logins);
    }

    [HttpPost]
    public async Task<IActionResult> PostLoginsAsync([FromBody] LoginsRequestDTO LoginsRequest)
    {
        string Token = await _tLoginsService.CrearAsync(LoginsRequest);
        return Ok(new{Token});
    }

    [HttpPut]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> PutLoginsAsync([FromBody] TloginsDTO tloginsDTO)
    {
        await _tLoginsService.ActualizarAsync(tloginsDTO.NLoginID, tloginsDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> DeleteLoginsAsync(int id)
    {
        await _tLoginsService.EliminarAsync(id);
        return NoContent();
    }
}