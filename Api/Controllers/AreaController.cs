using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api_Mediconnet.Api.Controllers;

[ApiController]
[Route("Api/Area")]
public class AreaController : ControllerBase
{
    private readonly ITAreaService _areaService;
    public AreaController(ITAreaService areaService)
    {
        _areaService = areaService;
    }
    
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetAreaAsync()
    {
        var areas = await _areaService.GetAreaDTOsAsync();
        return Ok(areas);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> GetAreaIdAsync(int id)
    {
        var area = await _areaService.GetAreaIdDTOsAsync(id);
        return Ok(area);
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> PostAreaAsync([FromBody] TAreaDTO areaDto)
    {
        await _areaService.CrearAsync(areaDto);
        return Ok();
    }

    [HttpPut]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> PutAreaAsync([FromBody] TAreaDTO areaDto)
    {
        await _areaService.ActualizarAsync(areaDto.AreaID, areaDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult> DeleteAreaAsync(int id)
    {
        await _areaService.EliminarAsync(id);
        return NoContent();
    }
}