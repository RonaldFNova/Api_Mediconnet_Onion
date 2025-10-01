using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.Interfaces;

namespace Api_Mediconnet.Application.Services;

public class TLoginsService : ITLoginsService
{
    private readonly ITLoginsRepository _tLoginsRepository;
    private readonly IHashPasswordService _hashPasswordService;
    private readonly IJwtTokenIdService _jwtTokenIdService;
    private readonly IAppLogger<TLoginsService> _appLogger;

    public TLoginsService(ITLoginsRepository tLoginsRepository, IHashPasswordService hashPasswordService, IJwtTokenIdService jwtTokenIdService, IAppLogger<TLoginsService> appLogger)
    {
        _tLoginsRepository = tLoginsRepository;
        _hashPasswordService = hashPasswordService;
        _jwtTokenIdService = jwtTokenIdService;
        _appLogger = appLogger;
    }

    public async Task<IEnumerable<TloginsDTO>> GetLoginsDTOsAsync()
    {
        var Logins = await _tLoginsRepository.GetLoginsAsync();

        _appLogger.LogInformation("Se recuperó la lista completa de Logins.");

        return Logins.Select(e => new TloginsDTO
        {
            LoginID = e.NLoginID,
            FechaLogin = e.DFechaLogin
        });
    }

    public async Task<TloginsDTO?> GetLoginsIdDTOsAsync(int id)
    {
        var logins = await _tLoginsRepository.GetLoginsIdAsync(id);

        if (logins == null)
        {
            _appLogger.LogError("No se encontró un login con el ID {id}.", id);
            return null;
        }
        
        _appLogger.LogInformation("Login con ID {LoginId} recuperado correctamente.", logins.NLoginID);

        return new TloginsDTO
        {
            LoginID = logins.NLoginID,
            FechaLogin = logins.DFechaLogin
        };
    }

    public async Task<StatusCodeDTO> CrearAsync(LoginsRequestDTO loginsRequest)
    {
        var user = await _tLoginsRepository.GetByEmailAsync(loginsRequest.Email);

        if (user == null)
        {
            _appLogger.LogError("No se encontró un usuario con el Email {Email}.", loginsRequest.Email);

            return new StatusCodeDTO
            {
                StatusCode = 404,
                Token = null,
                Mensaje = "Correo incorrecto",
            };
        }

        var result = _hashPasswordService.Verificar(loginsRequest.Password, user.CPassword);

        if (!result)
        {
            _appLogger.LogError("Contraseña incorrecta para el usuario con Email {Email}.", loginsRequest.Email);

            return new StatusCodeDTO
            {
                StatusCode = 401,
                Token = null,
                Mensaje = "Contraseña incorrecta"
            };
        }

        var NewLogins = new TLogins
        {
            DFechaLogin = DateTime.UtcNow,
            NUsuarioFK = user.NUsuarioID
        };


        await _tLoginsRepository.AddAsync(NewLogins);
        await _tLoginsRepository.SaveChangeAsync();

        _appLogger.LogInformation("Login con ID {LoginId} creado exitosamente.", NewLogins.NLoginID);

        if (user.NEstadoVerificacionFK == 1)
        {
            return new StatusCodeDTO
            {
                StatusCode = 200,
                Token = null,
                Mensaje = "Login ingresado correctamente",
                VerificadoEmail = false
            };
        }
        else
        {
            var token = _jwtTokenIdService.GenerarToken(user.NUsuarioID.ToString(), user.Rol.CNombre);

            return new StatusCodeDTO
            {
                StatusCode = 200,
                Token = token,
                Mensaje = "Login ingresado correctamente",
                VerificadoEmail = true
            };
        }

    }

    public async Task ActualizarAsync(int id, TloginsDTO DTOs)
    {
        var logins = await _tLoginsRepository.GetLoginsIdAsync(id);

        if (logins == null)
        {
            _appLogger.LogError("Error al actualizar el login con ID {id}: no existe en el sistema.", id);
            return;
        }

        logins.DFechaLogin = DTOs.FechaLogin;

        _tLoginsRepository.Update(logins);
        await _tLoginsRepository.SaveChangeAsync();

        _appLogger.LogInformation("Login con ID {LoginId} actualizado correctamente.", logins.NLoginID);
    }

    public async Task EliminarAsync(int id)
    {
        var logins = await _tLoginsRepository.GetLoginsIdAsync(id);

        if (logins == null)
        {
            _appLogger.LogError("Error al eliminar el login con ID {id}: no existe en el sistema.", id);
            return;
        }

        _tLoginsRepository.Delete(logins);
        await _tLoginsRepository.SaveChangeAsync();

        _appLogger.LogInformation("Usuario con ID {LoginId} eliminado correctamente.", logins.NLoginID);
    }
}