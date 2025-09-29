using Api_Mediconnet.Application.Interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.Interfaces;
using Api_Mediconnet.Domain.Enums;

namespace Api_Mediconnet.Application.Services;

public class PasswordResetService : IPasswordResetService
{
    private ITUsuarioRepository _tUsuarioRepository;
    private ITCodigoVerificacionRepository _tCodigoVerificacionRepository;
    private IPasswordSendResetService _passwordSendResetService;
    private readonly IAppLogger<PasswordResetService> _appLogger;

    public PasswordResetService(ITUsuarioRepository tUsuarioRepository, ITCodigoVerificacionRepository tCodigoVerificacionRepository,  IPasswordSendResetService passwordSendResetService)
    {
        _passwordSendResetService = passwordSendResetService;
        _tCodigoVerificacionRepository = tCodigoVerificacionRepository;
        _tUsuarioRepository = tUsuarioRepository;
    }

    public async Task GenerarTokenResetAsync(string email)
    {
        var usuario = await _tUsuarioRepository.GetUsuarioEmailAsync(email);

        if (usuario == null)
        {
            _appLogger.LogError("No se encontro usuario con el Email {email}",email);
            throw new Exception("Usuario no encontrado");
        }    

        var token = Guid.NewGuid().ToString("N");

        var codigo = new TCodigoVerificacion
        {
            NUsuarioFK = usuario.NUsuarioID,
            ETipoCodigo = Enum.Parse<TipoCodigoVerificacion>("PasswordReset"),
            DFechaExpiracion = DateTime.UtcNow.AddMinutes(15),
            CCodigo = token,
            BUsado = false,
            NIntentos = 0,
            DFechaCreacion = DateTime.UtcNow
        };

        await _tCodigoVerificacionRepository.AddAsync(codigo);
        await _tCodigoVerificacionRepository.SaveChangesAsync();

        _appLogger.LogInformation("Usuario con el ID {id} se envia codigo para cambiar contraseña", usuario.NUsuarioID);

        await _passwordSendResetService.SendEmailResetPasswordAsync(usuario.CEmail, usuario.CNombre + " " + usuario.CApellido, token);

    }
}


