using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;

namespace Api_Mediconnet.Application.Services;

public class TGrupoSanguineoService : ITGrupoSanguineoService
{
    private readonly ITGrupoSanguineoRepository _tGrupoSanguineoRepository;
    private readonly IAppLogger<TGrupoSanguineoService> _appLogger;

    public TGrupoSanguineoService(ITGrupoSanguineoRepository tGrupoSanguineoRepository, IAppLogger<TGrupoSanguineoService> appLogger)
    {
        _tGrupoSanguineoRepository = tGrupoSanguineoRepository;
        _appLogger = appLogger;
    }

    public async Task<IEnumerable<TGrupoSanguineoDTO>> GetGrupoSanguineoDTOsAsync()
    {
        var grupoSanguineo = await _tGrupoSanguineoRepository.GetGrupoSanguineoAsync();

        _appLogger.LogInformation("Se recuperó la lista completa de Grupos Sanguineos.");

        return grupoSanguineo.Select(u => new TGrupoSanguineoDTO
        {
            GrupoSanguineoID = u.NGrupoSanguineoID,
            Nombre = u.CNombre
        });
    }

    public async Task<TGrupoSanguineoDTO?> GetGrupoSanguineoIdDTOsAsync(int id)
    {
        var grupoSanguineo = await _tGrupoSanguineoRepository.GetGrupoSanguineoIdAsync(id);

        if (grupoSanguineo == null)
        {
            _appLogger.LogError("No se encontró un grupo sanguineo con el ID {id}.", id);
            return null;
        }

        _appLogger.LogInformation("Grupo Sanguineo con ID {GrupoSanguineoId} recuperado correctamente.", grupoSanguineo.NGrupoSanguineoID);

        return new TGrupoSanguineoDTO
        {
            GrupoSanguineoID = grupoSanguineo.NGrupoSanguineoID,
            Nombre = grupoSanguineo.CNombre
        };
    }

    public async Task CrearAsync(TGrupoSanguineoDTO DTOs)
    {
        var grupoSanguineo = new TGrupoSanguineo
        {
            CNombre = DTOs.Nombre
        };

        await _tGrupoSanguineoRepository.AddAsync(grupoSanguineo);
        await _tGrupoSanguineoRepository.SaveChangeAsync();

        _appLogger.LogInformation("Grupo Sanguineo con ID {GrupoSanguineoId} creado exitosamente.", grupoSanguineo.NGrupoSanguineoID);
    }

    public async Task ActualizarAsync(int id, TGrupoSanguineoDTO DTOs)
    {
        var grupoSanguineo = await _tGrupoSanguineoRepository.GetGrupoSanguineoIdAsync(id);

        if (grupoSanguineo == null)
        {
            _appLogger.LogError(null, "Error al actualizar el grupo sanguineo con ID {id}: no existe en el sistema.", id);
            return;
        }

        grupoSanguineo.CNombre = DTOs.Nombre;

        _tGrupoSanguineoRepository.Update(grupoSanguineo);
        await _tGrupoSanguineoRepository.SaveChangeAsync();

        _appLogger.LogInformation("Grupo Sanguineo con ID {GrupoSanguineoId} actualizado correctamente.", grupoSanguineo.NGrupoSanguineoID);
    }

    public async Task EliminarAsync(int id)
    {
        var grupoSanguineo = await _tGrupoSanguineoRepository.GetGrupoSanguineoIdAsync(id);

        if (grupoSanguineo == null)
        {
            _appLogger.LogError(null, "Error al eliminar el grupo sanguineo con ID {id}: no existe en el sistema.", id);
            return;
        }

        _tGrupoSanguineoRepository.Delete(grupoSanguineo);
        await _tGrupoSanguineoRepository.SaveChangeAsync();

        _appLogger.LogInformation("Grupo Sanguineo con ID {GrupoSanguineoId} eliminado correctamente.", grupoSanguineo.NGrupoSanguineoID);
    }
}