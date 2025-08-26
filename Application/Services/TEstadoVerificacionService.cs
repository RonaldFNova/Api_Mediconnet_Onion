using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Domain.interfaces;
using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Application.Services;

public class TEstadoVerificacionService : ITEstadoVerificacionService
{
    private readonly ITEstadoVerificacionRepository _tEstadoVerificacionRepository;
    private readonly IAppLogger<TEstadoVerificacionService> _appLogger;

    public TEstadoVerificacionService(ITEstadoVerificacionRepository tEstadoVerificacionRepository, IAppLogger<TEstadoVerificacionService> appLogger)
    {
        _tEstadoVerificacionRepository = tEstadoVerificacionRepository;
        _appLogger = appLogger;
    }

    public async Task<IEnumerable<TEstadoVerificacionDTO>> GetEstadoVerificacionDTOsAsync()
    {
        var estadoVerificacion = await _tEstadoVerificacionRepository.GetEstadoVerificacionAsync();

        _appLogger.LogInformation("Se recuperó la lista completa de Estados de Verificación.");

        return estadoVerificacion.Select(u => new TEstadoVerificacionDTO
        {
            EstadoVerificacionID = u.NEstadoVerificacionID,
            Nombre = u.CNombre
        });
    }

    public async Task<TEstadoVerificacionDTO?> GetEstadoVerificacionIdDTOsAsync(int id)
    {
        var estadoVerificacion = await _tEstadoVerificacionRepository.GetEstadoVerificacionIdAsync(id);

        if (estadoVerificacion == null)
        {
            _appLogger.LogError("No se encontró un estado de verificación con el ID {id}.", id);
            return null;
        }

        _appLogger.LogInformation("Estado de Verificación con ID {EstadoVerificacionId} recuperado correctamente.", estadoVerificacion.NEstadoVerificacionID);

        return new TEstadoVerificacionDTO
        {
            EstadoVerificacionID = estadoVerificacion.NEstadoVerificacionID,
            Nombre = estadoVerificacion.CNombre
        };
    }

    public async Task CrearAsync(TEstadoVerificacionDTO DTOs)
    {
        var estadoVerificacion = new TEstadoVerificacion
        {
            CNombre = DTOs.Nombre
        };

        await _tEstadoVerificacionRepository.AddAsync(estadoVerificacion);
        await _tEstadoVerificacionRepository.SaveChangeAsync();

        _appLogger.LogInformation("Estado de Verificación con ID {EstadoVerificacionId} creado exitosamente.", estadoVerificacion.NEstadoVerificacionID);
    }

    public async Task ActualizarAsync(int id, TEstadoVerificacionDTO DTOs)
    {
        var estadoVerificacion = await _tEstadoVerificacionRepository.GetEstadoVerificacionIdAsync(id);

        if (estadoVerificacion == null)
        {
            _appLogger.LogError(null, "Error al actualizar el estado de verificación con ID {id}: no existe en el sistema.", id);
            return;
        }

        estadoVerificacion.CNombre = DTOs.Nombre;

        _tEstadoVerificacionRepository.Update(estadoVerificacion);
        await _tEstadoVerificacionRepository.SaveChangeAsync();

        _appLogger.LogInformation("Estado de Verificación con ID {EstadoVerificacionId} actualizado correctamente.", estadoVerificacion.NEstadoVerificacionID);
    }

    public async Task EliminarAsync(int id)
    {
        var estadoVerificacion = await _tEstadoVerificacionRepository.GetEstadoVerificacionIdAsync(id);

        if (estadoVerificacion == null)
        {
            _appLogger.LogError(null, "Error al eliminar el estado de verificación con ID {id}: no existe en el sistema.", id);
            return;
        }

        _tEstadoVerificacionRepository.Delete(estadoVerificacion);
        await _tEstadoVerificacionRepository.SaveChangeAsync();

        _appLogger.LogInformation("Estado de Verificación con ID {EstadoVerificacionId} eliminado correctamente.", estadoVerificacion.NEstadoVerificacionID);
    }
}
