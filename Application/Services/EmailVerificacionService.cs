using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Api_Mediconnet.Domain.Interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.Enums;

namespace Api_Mediconnet.Application.Services;

public class EmailVerificacionService : IEmailVerificacionService
{
    private readonly ITCodigoVerificacionRepository _tCodigoVerificacionRepository;
    private readonly IEmailVerificacionSender _emailVerificacionSender;
    private readonly IAppLogger<EmailVerificacionService> _appLogger;
    private readonly ITUsuarioRepository _tUsuarioRepository;
    private readonly IJwtTokenIdService _jwtTokenIdService;
    public EmailVerificacionService(ITCodigoVerificacionRepository tCodigoVerificacionRepository, IAppLogger<EmailVerificacionService> appLogger, IEmailVerificacionSender emailVerificacionSender, ITUsuarioRepository tUsuarioRepository, IJwtTokenIdService jwtTokenIdService)
    {
        _tCodigoVerificacionRepository = tCodigoVerificacionRepository;
        _appLogger = appLogger;
        _emailVerificacionSender = emailVerificacionSender;
        _tUsuarioRepository = tUsuarioRepository;
        _jwtTokenIdService = jwtTokenIdService;
    }

    public async Task<StatusCodeDTO> GenerarCodigoVerificacionAsync(string email)
    {

        var usuario = await _tUsuarioRepository.GetUsuarioEmailAsync(email);

        if (usuario == null)
        {
            _appLogger.LogInformation("Usuario con el Email {email} no existe", email);
            return new StatusCodeDTO
            {
                Mensaje = "No existe usuario con ese email",
                StatusCode = 404
            };
            throw new Exception("Usuario no encontrado");
        }

        if (usuario.NEstadoVerificacionFK == 2)
        {
            _appLogger.LogInformation("Usuario con el Email {email} ya esta verificado", email);
            return new StatusCodeDTO
            {
                Mensaje = "El usuario ya está verificado",
                StatusCode = 409
            };
        }

        var random = new Random();
        string codigo_verificacion = random.Next(100000, 999999).ToString();

        var codigo = new TCodigoVerificacion
        {
            CCodigo = codigo_verificacion,
            NUsuarioFK = usuario.NUsuarioID,
            DFechaExpiracion = DateTime.UtcNow.AddMinutes(15),
            BUsado = false,
            DFechaCreacion = DateTime.UtcNow,
            ETipoCodigo = Enum.Parse<TipoCodigoVerificacion>("Email"),
            NIntentos = 0
        };

        await _tCodigoVerificacionRepository.AddAsync(codigo);
        await _tCodigoVerificacionRepository.SaveChangesAsync();

        _appLogger.LogInformation("Código de Verificación de correo creado correctamente al usuario con el ID {CodigoVerificacionID}.", usuario.NUsuarioID);

        await _emailVerificacionSender.SendVerificationCodeAsync(usuario.CEmail, usuario.CNombre, codigo.CCodigo);

        return new StatusCodeDTO
        {
            Mensaje = "Email enviado correctamente",
            StatusCode = 200
        };

    }


    public async Task<StatusCodeDTO> ValidarCodigoVerificacionAsync(string email, string codigo)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(codigo))
        {
            return new StatusCodeDTO
            {
                StatusCode = 400,
                Mensaje = "Minimo tienes que enviar el codigo y el correo para que funcione"
            };
        }

        var codigoVerificacion = await _tCodigoVerificacionRepository.GetCodigoUserEmailAsync(email);

        if (codigoVerificacion == null)
        {
            _appLogger.LogError("No se encontró un código de verificación con el Email {email} para validar.", email);
            return new StatusCodeDTO
            {
                StatusCode = 404,
                Mensaje = "El código no fue encontrado"
            };
        }

        if (codigoVerificacion.BUsado)
        {
            _appLogger.LogWarning("El código de verificación con Email {email} ya ha sido usado.", email);
            return new StatusCodeDTO
            {
                StatusCode = 409,
                Mensaje = "El código ya ha sido usado"
            };
        }

        if (DateTime.UtcNow > codigoVerificacion.DFechaExpiracion)
        {
            _appLogger.LogWarning("El código de verificación con Email {email} ha expirado.", email);
            codigoVerificacion.BUsado = true;

            _tCodigoVerificacionRepository.Update(codigoVerificacion);
            await _tCodigoVerificacionRepository.SaveChangesAsync();

            return new StatusCodeDTO
            {
                StatusCode = 410,
                Mensaje = "El código ya ha expirado"
            };
        }

        if (codigoVerificacion.CCodigo == codigo)
        {
            codigoVerificacion.BUsado = true;
            _appLogger.LogWarning("El código de verificación con ID {id} es correcto. Intento {Intentos}.", codigoVerificacion.NCodigoVerificacionID, codigoVerificacion.NIntentos);

            _tCodigoVerificacionRepository.Update(codigoVerificacion);
            await _tCodigoVerificacionRepository.SaveChangesAsync();

            var usuario = await _tUsuarioRepository.GetUsuarioIdAsync(codigoVerificacion.NUsuarioFK);
            usuario.NEstadoVerificacionFK = 2;

            _tUsuarioRepository.Update(usuario);
            await _tUsuarioRepository.SaveChangesAsync();

            string usuarioId = usuario.NUsuarioID.ToString();
            string rolNombre = await _tUsuarioRepository.GetRolNombreByUsuarioIdAsync(usuario.NUsuarioID);

            string token = _jwtTokenIdService.GenerarToken(usuarioId, rolNombre);

            return new StatusCodeDTO
            {
                StatusCode = 200,
                Mensaje = "Código verificado correctamente",
                Token = token
            }; 
        }
        else
        {
            codigoVerificacion.NIntentos += 1;

            if (codigoVerificacion.NIntentos > 9)
            {
                codigoVerificacion.BUsado = true;
                _appLogger.LogWarning("El código de verificación con Email {email} ha sido bloqueado por exceder el número de intentos.", email);
            }

            _appLogger.LogWarning("El código de verificación con Email {email} es incorrecto.", email);

            _tCodigoVerificacionRepository.Update(codigoVerificacion);
            await _tCodigoVerificacionRepository.SaveChangesAsync();

            return new StatusCodeDTO
            {
                StatusCode = 400,
                Mensaje = "El código ingresado no es válido"
            };
        }
        
        
    }


}