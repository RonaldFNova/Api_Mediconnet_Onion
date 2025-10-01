using Api_Mediconnet.Application.Interfaces;
using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.Interfaces;

namespace Api_Mediconnet.Application.Services;

public class TUsuarioService : ITUsuarioService
{
    private readonly IHashPasswordService _servicioHashPassword;
    private readonly ITUsuarioRepository _tUsuarioRepository;
    private readonly IJwtTokenIdService _jwtTokenIdService;
    private readonly IAppLogger<TUsuarioService> _appLogger;
    public TUsuarioService(ITUsuarioRepository tUsuarioRepository, IHashPasswordService servicioHashPassword, IJwtTokenIdService jwtTokenIdService, IAppLogger<TUsuarioService> appLogger)
    {
        _tUsuarioRepository = tUsuarioRepository;
        _servicioHashPassword = servicioHashPassword;
        _jwtTokenIdService = jwtTokenIdService;
        _appLogger = appLogger;
    }

    public async Task<IEnumerable<TUsuarioResponseDTO>> GetUsuarioAsync()
    {
        var Usuario = await _tUsuarioRepository.GetUsuarioAsync();

        _appLogger.LogInformation("Se recuperó la lista completa de Usuario.");

        return Usuario.Select(u => new TUsuarioResponseDTO
        {
            UsuarioID = u.NUsuarioID,
            Nombre = u.CNombre,
            Apellido = u.CApellido,
            Email = u.CEmail,
            RolFK = u.NRolFK,
            EstadoUsuarioFK = u.NEstadoUsuarioFK,
            EstadoVerificacionFK = u.NEstadoVerificacionFK
        });

    }

    public async Task<TUsuarioResponseDTO?> GetUsuarioIdAsync(int id)
    {
        var usuario = await _tUsuarioRepository.GetUsuarioIdAsync(id);
        if (usuario == null)
        {
            _appLogger.LogError("No se encontró un usuario con el ID {id}.", id);
            return null;
        }

        _appLogger.LogInformation("Usuario con ID {UsuarioId} recuperado correctamente.", usuario.NUsuarioID);

        return new TUsuarioResponseDTO
        {
            UsuarioID = usuario.NUsuarioID,
            Nombre = usuario.CNombre,
            Apellido = usuario.CApellido,
            Email = usuario.CEmail,
            RolFK = usuario.NRolFK,
            EstadoUsuarioFK = usuario.NEstadoUsuarioFK,
            EstadoVerificacionFK = usuario.NEstadoVerificacionFK
        };
    }

    public async Task<StatusCodeDTO> CrearAsync(TUsuarioCreateDTO dTO)
    {
        var email = await _tUsuarioRepository.GetUsuarioEmailAsync(dTO.Email);

        if (email != null)
        {
            _appLogger.LogError("El Email {email} ya esta registrado", dTO.Email);
            return new StatusCodeDTO
            {
                StatusCode = 409,
                Mensaje = "El correo ya está registrado",
                Token = ""
            }; 
        }

        var usuario = new TUsuario
        {
            CNombre = dTO.Nombre,
            CApellido = dTO.Apellido,
            CEmail = dTO.Email,
            CPassword = _servicioHashPassword.Hash(dTO.Password),
            NRolFK = 2, // Asignar rol de Paciente por defecto
            NEstadoUsuarioFK = 1,
            NEstadoVerificacionFK = 1, // Estado de verificación "No Verificado"
            DFechaRegistro = DateTime.UtcNow
        };

        await _tUsuarioRepository.AddAsync(usuario);
        await _tUsuarioRepository.SaveChangesAsync();

        _appLogger.LogInformation("Usuario con ID {UsuarioId} creado exitosamente.", usuario.NUsuarioID);

        return new StatusCodeDTO
        {
            StatusCode = 200,
            Mensaje = "Usuario creado correctamente",
            Token = ""
        }; 

    }

    public async Task ActualizarAsync(int id, TUsuarioCreateDTO dTO)
    {
        var usuario = await _tUsuarioRepository.GetUsuarioIdAsync(id);

        if (usuario == null)
        {
            _appLogger.LogError("Error al actualizar el usuario con ID {id}: no existe en el sistema.", id);
            return;
        }

        usuario.CNombre = dTO.Nombre;
        usuario.CApellido = dTO.Apellido;
        usuario.CEmail = dTO.Email;
        usuario.CPassword = dTO.Password;
        usuario.NRolFK = dTO.RolFK;
        usuario.NEstadoUsuarioFK = dTO.EstadoUsuarioFK;
        usuario.NEstadoVerificacionFK = dTO.EstadoVerificacionFK;

        _tUsuarioRepository.Update(usuario);
        await _tUsuarioRepository.SaveChangesAsync();

        _appLogger.LogInformation("Usuario con ID {UsuarioId} actualizado correctamente.", usuario.NUsuarioID);
    }

    public async Task EliminarAsync(int id)
    {
        var usuario = await _tUsuarioRepository.GetUsuarioIdAsync(id);

        if (usuario == null)
        {
            _appLogger.LogError("Error al eliminar el usuario con ID {id}: no existe en el sistema.", id);
            return;
        }

        _tUsuarioRepository.Delete(usuario);
        await _tUsuarioRepository.SaveChangesAsync();

        _appLogger.LogInformation("Usuario con ID {UsuarioId} eliminado correctamente.", usuario.NUsuarioID);
    }

    public async Task<UsuarioEmailDTO> GetEmailAsync(int id)
    {
        var usuario = await _tUsuarioRepository.GetUsuarioIdAsync(id);
        if (usuario == null)
        {
            _appLogger.LogError("No se encontró un usuario con el ID {id}.", id);
            return null;
        }

        _appLogger.LogInformation("Email del usuario con ID {UsuarioId} recuperado correctamente.", usuario.NUsuarioID);

        return new UsuarioEmailDTO
        {
            NombreCompleto = $"{usuario.CNombre} {usuario.CApellido}",
            Email = usuario.CEmail,
            UsuarioID = usuario.NUsuarioID
        };
    }

    public async Task<UsuarioEmailDTO?> GetUsuarioEmailAsync(string email)
    {
        var usuario = await _tUsuarioRepository.GetUsuarioEmailAsync(email);

        if (usuario == null)
        {
            _appLogger.LogError("No se encontró un usuario con el Email {email}.", email);
            return null;
        }

        _appLogger.LogInformation("ID del usuario con Email {Email} recuperado correctamente.", usuario.CEmail);

        return new UsuarioEmailDTO
        {
            NombreCompleto = $"{usuario.CNombre} {usuario.CApellido}",
            Email = usuario.CEmail,
            UsuarioID = usuario.NUsuarioID
        };
    }
}