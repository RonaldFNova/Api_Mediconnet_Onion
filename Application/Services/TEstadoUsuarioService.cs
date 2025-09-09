using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.Interfaces;

namespace Api_Mediconnet.Application.Services;

public class TEstadoUsuarioervice : ITEstadoUsuarioervice
{
    private readonly ITEstadoUsuarioRepository _tEstadoUsuarioRepository;
     private readonly IAppLogger<TEstadoUsuarioervice> _appLogger;

    public TEstadoUsuarioervice(ITEstadoUsuarioRepository tEstadoUsuarioRepository, IAppLogger<TEstadoUsuarioervice> appLogger)
    {
        _tEstadoUsuarioRepository = tEstadoUsuarioRepository;
        _appLogger = appLogger;
    }

    public async Task<IEnumerable<TEstadoUsuarioDTO>> GetEstadoUsuarioDTOsAsync()
    {
        var estadoUsuario = await _tEstadoUsuarioRepository.GetEstadoUsuarioAsync();

        _appLogger.LogInformation("Se recuperó la lista completa de Estados de Usuario.");

        return estadoUsuario.Select(u => new TEstadoUsuarioDTO
        {
            EstadoUsuarioID = u.NEstadoUsuarioID,
            Estado = u.CNombre
        });
    }

    public async Task<TEstadoUsuarioDTO?> GetEstadoUsuarioIdDTOsAsync(int id)
    {
        var estadoUsuario = await _tEstadoUsuarioRepository.GetEstadoUsuarioIdAsync(id);

        if (estadoUsuario == null)
        {
            _appLogger.LogError("No se encontró un estado de usuario con el ID {id}.", id);
            return null;
        }
        
        _appLogger.LogInformation("Estado de Usuario con ID {EstadoUsuarioId} recuperado correctamente.", estadoUsuario.NEstadoUsuarioID);

        return new TEstadoUsuarioDTO
        {
            EstadoUsuarioID = estadoUsuario.NEstadoUsuarioID,
            Estado = estadoUsuario.CNombre
        };
    }

    public async Task CrearAsync(TEstadoUsuarioDTO DTOs)
    {
        var estadoUsuario = new TEstadoUsuario
        {
            CNombre = DTOs.Estado
        };

        await _tEstadoUsuarioRepository.AddAsync(estadoUsuario);
        await _tEstadoUsuarioRepository.SaveChangeAsync();

        _appLogger.LogInformation("Estado de Usuario con ID {EstadoUsuarioId} creado exitosamente.", estadoUsuario.NEstadoUsuarioID);
    }

    public async Task ActualizarAsync(int id, TEstadoUsuarioDTO DTOs)
    {
        var estadoUsuario = await _tEstadoUsuarioRepository.GetEstadoUsuarioIdAsync(id);

        if (estadoUsuario == null)
        {
            _appLogger.LogError("Error al actualizar el estado de usuario con ID {id}: no existe en el sistema.", id);
            return;
        }

        estadoUsuario.CNombre = DTOs.Estado;

        _tEstadoUsuarioRepository.Update(estadoUsuario);
        await _tEstadoUsuarioRepository.SaveChangeAsync();

        _appLogger.LogInformation("Estado de Usuario con ID {EstadoUsuarioId} actualizado correctamente.", estadoUsuario.NEstadoUsuarioID);
    }

    public async Task EliminarAsync(int id)
    {
        var estadoUsuario = await _tEstadoUsuarioRepository.GetEstadoUsuarioIdAsync(id);

        if (estadoUsuario == null)
        {
            _appLogger.LogError("Error al eliminar el estado de usuario con ID {id}: no existe en el sistema.", id);
            return;
        }

        _tEstadoUsuarioRepository.Delete(estadoUsuario);
        await _tEstadoUsuarioRepository.SaveChangeAsync();

        _appLogger.LogInformation("Estado de Usuario con ID {EstadoUsuarioId} eliminado correctamente.", estadoUsuario.NEstadoUsuarioID);
    }
}