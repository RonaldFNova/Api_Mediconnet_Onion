using Api_Mediconnet.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Api.Controllers;

[Route("Api/EmailCode")]
[ApiController]
public class EmailCodeController : ControllerBase
{
    private readonly IEmailService _emailService;
    private readonly ITUsuarioService _tUsuarioService;
    private readonly IPasswordResetService _passwordResetService;
    private readonly ITCodigoVerificacionService _codigoVerificacionService;

    public EmailCodeController(IEmailService emailService, ITUsuarioService tUsuarioService, ITCodigoVerificacionService codigoVerificacionService, IPasswordResetService passwordResetService)
    {
        _codigoVerificacionService = codigoVerificacionService;
        _emailService = emailService;
        _tUsuarioService = tUsuarioService;
        _passwordResetService = passwordResetService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> PostEmailCode()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var usuarioEmail = await _tUsuarioService.GetEmailAsync(int.Parse(userId));

        await _emailService.SendEmailCodeAsync(usuarioEmail.Email, usuarioEmail.NombreCompleto, usuarioEmail.UsuarioID);
        return Ok();
    }

    [HttpPost("Email")]
    public async Task<ActionResult> PostEmailCode2([FromBody] EmailRequestDTO request)
    {
        var usuarioEmail = await _tUsuarioService.GetUsuarioEmailAsync(request.Email);

        await _emailService.SendEmailCodeAsync(usuarioEmail.Email, usuarioEmail.NombreCompleto, usuarioEmail.UsuarioID);
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