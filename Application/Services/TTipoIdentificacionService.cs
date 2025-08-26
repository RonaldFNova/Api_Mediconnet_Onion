using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Domain.interfaces;
using Api_Mediconnet.Domain.Entities;


namespace Api_Mediconnet.Application.Services;

public class TTipoIdentificacionService : ITipoIdentificacionService
{
    private readonly ITTipoIdentificacionRepository _tTipoIdentificacionRepository;
    private readonly IAppLogger<TTipoIdentificacionService> _appLogger;


    public TTipoIdentificacionService(ITTipoIdentificacionRepository tTipoIdentificacionRepository, IAppLogger<TTipoIdentificacionService> appLogger)
    {
        _tTipoIdentificacionRepository = tTipoIdentificacionRepository;
        _appLogger = appLogger;
    }

    public async Task<IEnumerable<TTipoIdentificacionDTO>> GetTipoIdentificacionDTOsAsync()
    {
        var TipoIdentificacion = await _tTipoIdentificacionRepository.GetTipoIdentificacionAsync();

        _appLogger.LogInformation("Se recuperó la lista completa de Identificaciones.");

        return TipoIdentificacion.Select(u => new TTipoIdentificacionDTO
        {
            TipoIdentificacionID = u.NTipoIdentificacionID,
            Nombre = u.CNombre
        });
    }
    
    public async Task<TTipoIdentificacionDTO?> GetTipoIdentificacionIdDTOsAsync(int id)
    {
        var TipoIdentificacion = await _tTipoIdentificacionRepository.GetTipoIdentificacionIdAsync(id);
        if (TipoIdentificacion == null)
        {
            _appLogger.LogError("No se encontró una identificacion con el ID {id}.", id);
            return null;
        }

        _appLogger.LogInformation("Identificacion con ID {IdentificacionId} recuperado correctamente.", TipoIdentificacion.NTipoIdentificacionID);

        return new TTipoIdentificacionDTO
        {
            TipoIdentificacionID = TipoIdentificacion.NTipoIdentificacionID,
            Nombre = TipoIdentificacion.CNombre
        };
    }

    public async Task CrearAsync(TTipoIdentificacionDTO DTO)
    {
        var TipoIdentificacion = new TTipoIdentificacion
        {
            CNombre = DTO.Nombre
        };

        await _tTipoIdentificacionRepository.AddAsync(TipoIdentificacion);
        await _tTipoIdentificacionRepository.SaveChangeAsync();

        _appLogger.LogInformation("Identificacion con ID {IdentificacionId} creado exitosamente.", TipoIdentificacion.NTipoIdentificacionID);
    }

    public async Task ActualizarAsync(int id, TTipoIdentificacionDTO DTO)
    {
        var TipoIdentificacion = await _tTipoIdentificacionRepository.GetTipoIdentificacionIdAsync(id);

        if (TipoIdentificacion == null)
        {
            _appLogger.LogError(null, "Error al actualizar la identificacion con ID {id}: no existe en el sistema.", id);
        }

        TipoIdentificacion.CNombre = DTO.Nombre;

        _tTipoIdentificacionRepository.Update(TipoIdentificacion);
        await _tTipoIdentificacionRepository.SaveChangeAsync();

        _appLogger.LogInformation("Identificacion con ID {IdentificacionId} actualizado correctamente.", TipoIdentificacion.NTipoIdentificacionID);

    }

    public async Task EliminarAsync(int id)
    {
        var TipoIdentificacion = await _tTipoIdentificacionRepository.GetTipoIdentificacionIdAsync(id);

        if (TipoIdentificacion == null)
        {
            _appLogger.LogError(null, "Error al eliminar la identificacion con ID {id}: no existe en el sistema.", id);
        }

        _tTipoIdentificacionRepository.Delete(TipoIdentificacion);
        await _tTipoIdentificacionRepository.SaveChangeAsync();

        _appLogger.LogInformation("Usuario con ID {IdentificacionId} eliminado correctamente.", TipoIdentificacion.NTipoIdentificacionID);

    }
}
