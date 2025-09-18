using Api_Mediconnet.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api_Mediconnet.Api.Controllers;

[Route("Api/Email")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;
    private readonly ITUsuarioService _tUsuarioService;
    public EmailController(IEmailService emailService, ITUsuarioService tUsuarioService)
    {
        _emailService = emailService;
        _tUsuarioService = tUsuarioService;
    }

    [HttpPost]
    [Authorize] 
    public async Task<ActionResult> PostEmailCode()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        var usuarioEmail = await _tUsuarioService.GetEmailAsync(int.Parse(userId));

        await _emailService.SendEmailCodeAsync(usuarioEmail.Email, usuarioEmail.NombreCompleto);
        return Ok();
    }
}