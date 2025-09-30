using Api_Mediconnet.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Api.Controllers;

[Route("Api/EmailCode")]
[ApiController]
public class EmailCodeController : ControllerBase
{
    private readonly IEmailVerificacionService _emailVerificacionService;
    private readonly ITUsuarioService _tUsuarioService;
    private readonly IPasswordResetService _passwordResetService;
    private readonly ITCodigoVerificacionService _codigoVerificacionService;

    public EmailCodeController(IEmailVerificacionService emailVerificacionService, ITUsuarioService tUsuarioService, ITCodigoVerificacionService codigoVerificacionService, IPasswordResetService passwordResetService)
    {
        _codigoVerificacionService = codigoVerificacionService;
        _emailVerificacionService = emailVerificacionService;
        _tUsuarioService = tUsuarioService;
        _passwordResetService = passwordResetService;
    }


    [HttpPost("Email")]
    public async Task<ActionResult> PostEmailCode([FromBody] EmailRequestDTO request)
    {
        await _emailVerificacionService.GenerarCodeVerificationAsync(request.Email);
        return Ok();
    }

    [HttpPost("Verificar")]
    [Authorize]
    public async Task<ActionResult> PostVerificarEmailCode([FromBody] VerificarCodigoRequestDTO request)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var reultado = await _codigoVerificacionService.ValidarCodigoVerificacionAsync(request.Codigo, int.Parse(userId), null);

        return StatusCode(reultado.StatusCode, new { mensaje = reultado.Mensaje });
    }

    [HttpPost("Verificar/Email")]
    public async Task<ActionResult> PostVerificarEmailCodeEmail([FromBody] VerificarCodigoEmailRequestDTO request)
    {
        var resultado = await _codigoVerificacionService.ValidarCodigoVerificacionAsync(request.Codigo, null, request.Email);

        return StatusCode(resultado.StatusCode, new { Mensaje = resultado.Mensaje, Token = resultado.Token });
    }
    [HttpPost("Forgot-Password")]
    public async Task<ActionResult> PostSendCodePasswordEmail([FromBody] EmailRequestDTO request)
    {
        await _passwordResetService.GenerarTokenResetAsync(request.Email);
        return Ok();
        
    }
}