using Api_Mediconnet.Application.Interfaces;
using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.Services;

public class PasswordResetService : IPasswordResetService
{
    private ITUsuarioService _tUsuarioService;
    private ITCodigoVerificacionService _tCodigoVerificacionService;
    private IPasswordResetSender _passwordResetSender;
    private readonly IAppLogger<PasswordResetService> _appLogger;

    public PasswordResetService(ITUsuarioService tUsuarioService, ITCodigoVerificacionService tCodigoVerificacionService, IPasswordResetSender passwordResetSender)
    {
        _passwordResetSender = passwordResetSender;
        _tCodigoVerificacionService = tCodigoVerificacionService;
        _tUsuarioService = tUsuarioService;
    }

    public async Task GenerarTokenResetAsync(string email)
    {
        var usuario = await _tUsuarioService.GetUsuarioEmailAsync(email);
   
        if (usuario == null)
        {
            _appLogger.LogError("No se encontro usuario con el Email {email}", email);
            throw new Exception("Usuario no encontrado");
        }

        var token = Guid.NewGuid().ToString("N");

        Console.WriteLine(token);

        var codigoDTO = new TCodigoVerificacionDTO
        {
            Codigo = token,
            UsuarioFK = usuario.UsuarioID,
            FechaExpiracion = DateTime.UtcNow.AddMinutes(15),
            FechaCreacion = DateTime.UtcNow,
            TipoCodigo = "PasswordReset",
            Usado = false,
            Intentos = 0
        };

        await _tCodigoVerificacionService.CrearAsync(codigoDTO);

        _appLogger.LogInformation("Usuario con el ID {id} se envia codigo para cambiar contraseña", usuario.UsuarioID);

        await _passwordResetSender.SendEmailResetPasswordAsync(usuario.Email, usuario.NombreCompleto, token);

    }
}


