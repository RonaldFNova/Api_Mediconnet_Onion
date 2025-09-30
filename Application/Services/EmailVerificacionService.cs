using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;

namespace Api_Mediconnet.Application.Services;

public class EmailVerificacionService : IEmailVerificacionService
{
    private readonly ITCodigoVerificacionService _tcodigoVerificacionService;
    private readonly IEmailVerificacionSender _emailVerificacionSender;
    private readonly IAppLogger<EmailVerificacionService> _appLogger;
    private readonly ITUsuarioService _usuarioService;
    public EmailVerificacionService(ITCodigoVerificacionService tcodigoVerificacionService, IAppLogger<EmailVerificacionService> appLogger, IEmailVerificacionSender emailVerificacionSender, ITUsuarioService usuarioService)
    {
        _tcodigoVerificacionService = tcodigoVerificacionService;
        _appLogger = appLogger;
        _emailVerificacionSender = emailVerificacionSender;
        _usuarioService = usuarioService;
    }

    public async Task GenerarCodeVerificationAsync(string email)
    {

        var usuario = await _usuarioService.GetUsuarioEmailAsync(email);

        if (usuario == null)
        {
            _appLogger.LogInformation("Usuario con el Email {email} no existe", email);
            throw new Exception("Usuario no encontrado");
        }

        var random = new Random();
        string codigo_verificacion = random.Next(100000, 999999).ToString();

        var codigoDTO = new TCodigoVerificacionDTO
        {
            Codigo = codigo_verificacion,
            UsuarioFK = usuario.UsuarioID,
            FechaExpiracion = DateTime.UtcNow.AddMinutes(15),
            FechaCreacion = DateTime.UtcNow,
            TipoCodigo = "Email",
            Usado = false,
            Intentos = 0
        };

        await _tcodigoVerificacionService.CrearAsync(codigoDTO);

        _appLogger.LogInformation("Código de Verificación de correo creado correctamente al usuario con el ID {CodigoVerificacionID}.", codigoDTO.UsuarioFK);

        await _emailVerificacionSender.SendVerificationCodeAsync(usuario.Email, usuario.NombreCompleto, codigoDTO.Codigo);
        
    }


}