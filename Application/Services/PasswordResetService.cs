using Api_Mediconnet.Application.Interfaces;
using Api_Mediconnet.Domain.Interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.Enums;

namespace Api_Mediconnet.Application.Services;

public class PasswordResetService : IPasswordResetService
{
    private ITUsuarioRepository _tUsuarioRepository;
    private ITCodigoVerificacionRepository _tCodigoVerificacionRepository;
    private IPasswordResetSender _passwordResetSender;
    private readonly IAppLogger<PasswordResetService> _appLogger;

    public PasswordResetService(ITUsuarioRepository tUsuarioRepository, ITCodigoVerificacionRepository tCodigoVerificacionRepository, IPasswordResetSender passwordResetSender, IAppLogger<PasswordResetService> appLogger)
    {
        _passwordResetSender = passwordResetSender;
        _tCodigoVerificacionRepository = tCodigoVerificacionRepository;
        _tUsuarioRepository = tUsuarioRepository;
        _appLogger = appLogger;
    }

    public async Task GenerarTokenResetAsync(string email)
    {
        var usuario = await _tUsuarioRepository.GetUsuarioEmailAsync(email);
   
        if (usuario == null)
        {
            _appLogger.LogError("No se encontro usuario con el Email {email}", email);
            throw new Exception("Usuario no encontrado");
        }

        var token = Guid.NewGuid().ToString("N");

        Console.WriteLine(token);

        var codigo = new TCodigoVerificacion
        {
            CCodigo = token,
            NUsuarioFK = usuario.NUsuarioID,
            DFechaExpiracion = DateTime.UtcNow.AddMinutes(15),
            BUsado = false,
            DFechaCreacion = DateTime.UtcNow,
            ETipoCodigo = Enum.Parse<TipoCodigoVerificacion>("PasswordReset"),
            NIntentos = 0
        };

        await _tCodigoVerificacionRepository.AddAsync(codigo);
        await _tCodigoVerificacionRepository.SaveChangesAsync();

        _appLogger.LogInformation("Usuario con el ID {id} se envia codigo para cambiar contraseña", usuario.NUsuarioID);

        await _passwordResetSender.SendEmailResetPasswordAsync(usuario.CEmail, usuario.CNombre, token);

    }
}


