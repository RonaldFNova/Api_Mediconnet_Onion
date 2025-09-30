using Api_Mediconnet.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Api.Controllers;

[Route("Api/EmailVerificacion")]
[ApiController]
public class EmailVerificacionController : ControllerBase
{
    private readonly IEmailVerificacionService _emailVerificacionService;

    public EmailVerificacionController(IEmailVerificacionService emailVerificacionService)
    {
        _emailVerificacionService = emailVerificacionService;
    }

    [HttpPost("Enviar")]
    public async Task<ActionResult> RequestCode([FromBody] EmailRequestDTO request)
    {
        await _emailVerificacionService.GenerarCodigoVerificacionAsync(request.Email);
        return Ok();
    }

    [HttpPost("Verificar")]
    public async Task<ActionResult> VerifyCode([FromBody] VerificarCodigoEmailRequestDTO request)
    {
        var resultado = await _emailVerificacionService.ValidarCodigoVerificacionAsync(request.Email, request.Codigo);
        return StatusCode(resultado.StatusCode, new { Mensaje = resultado.Mensaje, Token = resultado.Token });
    }
}
