using Api_Mediconnet.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Api.Controllers;


[Route("Api/PasswordReset")]
[ApiController]
public class PasswordResetController : ControllerBase
{
    private readonly IPasswordResetService _passwordResetService;

    public PasswordResetController(IPasswordResetService passwordResetService)
    {
        _passwordResetService = passwordResetService;
    }

    [HttpPost("Enviar")]
    public async Task<ActionResult> RequestReset([FromBody] EmailRequestDTO request)
    {
        await _passwordResetService.GenerarTokenResetAsync(request.Email);
        return Ok();
    }

    [HttpPost("Verificar")]
    public async Task<ActionResult> VerifyToken([FromBody] PasswordResetDTO request)
    {
        await _passwordResetService.ResetPasswordAsync(request);
        return Ok();
    }

}
