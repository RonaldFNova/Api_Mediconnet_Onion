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
    private readonly ITCodigoVerificacionService _codigoVerificacionService;

    public EmailCodeController(IEmailService emailService, ITUsuarioService tUsuarioService, ITCodigoVerificacionService codigoVerificacionService)
    {
        _codigoVerificacionService = codigoVerificacionService;
        _emailService = emailService;
        _tUsuarioService = tUsuarioService;
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

    [HttpPost("Verificar")]
    [Authorize]
    public async Task<ActionResult> VerificarEmailCode([FromBody] VerificarCodigoRequestDTO request)
    {     
        Console.WriteLine("LOLLLLLLLL");
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        bool isValid = await _codigoVerificacionService.ValidarCodigoVerificacionAsync(int.Parse(userId), request.Codigo);

        return Ok(isValid);
    }
    
}