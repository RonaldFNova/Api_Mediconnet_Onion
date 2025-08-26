using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;

namespace Api_Mediconnet.Application.Services;

public class TEspecialidadService : ITEspecialidadService
{
    private readonly ITEspecialidadRepository _tEspecialidadRepository;
     private readonly IAppLogger<TEspecialidadService> _appLogger;


    public TEspecialidadService(ITEspecialidadRepository tEspecialidadRepository, IAppLogger<TEspecialidadService> appLogger)
    {
        _tEspecialidadRepository = tEspecialidadRepository;
        _appLogger = appLogger;
    }


    public async Task<IEnumerable<TEspecialidadDTO>> GetEspecialidadDTOsAsync()
    {
        var especialidad = await _tEspecialidadRepository.GetEspecialidadAsync();

        _appLogger.LogInformation("Se recuperó la lista completa de Especialidades.");

        return especialidad.Select(e => new TEspecialidadDTO
        {
            EspecialidadID = e.NEspecialidadID,
            Nombre = e.CNombre,
            Descripcion = e.CDescripcion
        });
    }

    public async Task<TEspecialidadDTO?> GetEspecialidadIdDTOsAsync(int id)
    {
        var especialidad = await _tEspecialidadRepository.GetEspecialidadIdAsync(id);

        if (especialidad == null)
        {
            _appLogger.LogError("No se encontró una especialidad con el ID {id}.", id);
            return null;
        }
        
        _appLogger.LogInformation("Especialidad con ID {EspecialidadId} recuperado correctamente.", especialidad.NEspecialidadID);

        return new TEspecialidadDTO
        {
            EspecialidadID = especialidad.NEspecialidadID,
            Nombre = especialidad.CNombre,
            Descripcion = especialidad.CDescripcion
        };
    }

    public async Task CrearAsync(TEspecialidadDTO especialidadDTO)
    {
        var especialidad = new TEspecialidad
        {
            CNombre = especialidadDTO.Nombre,
            CDescripcion = especialidadDTO.Descripcion
        };

        await _tEspecialidadRepository.AddAsync(especialidad);
        await _tEspecialidadRepository.SaveChangesAsync();

        _appLogger.LogInformation("Especialidad con ID {EspecialidadId} creado exitosamente.", especialidad.NEspecialidadID);
    }

    public async Task ActualizarAsync(int id, TEspecialidadDTO especialidadDTO)
    {
        var especialidad = await _tEspecialidadRepository.GetEspecialidadIdAsync(id);

        if (especialidad == null)
        {
            _appLogger.LogError(null, "Error al actualizar la especialidad con ID {id}: no existe en el sistema.", id);
            return;
        }

        especialidad.CNombre = especialidadDTO.Nombre;
        especialidad.CDescripcion = especialidadDTO.Descripcion;

        _tEspecialidadRepository.Update(especialidad);
        await _tEspecialidadRepository.SaveChangesAsync();

        _appLogger.LogInformation("Especialidad con ID {EspecialidadId} actualizado correctamente.", especialidad.NEspecialidadID);
    }

    public async Task EliminarAsync(int id)
    {
        var especialidad = await _tEspecialidadRepository.GetEspecialidadIdAsync(id);

        if (especialidad == null)
        {
            _appLogger.LogError(null, "Error al eliminar la especialidad con ID {id}: no existe en el sistema.", id);
            return;
        }

        _tEspecialidadRepository.Delete(especialidad);
        await _tEspecialidadRepository.SaveChangesAsync();

        _appLogger.LogInformation("Especialidad con ID {EspecialidadId} eliminado correctamente.", especialidad.NEspecialidadID);
    }
}