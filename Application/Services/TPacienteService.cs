using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.Interfaces;

namespace Api_Mediconnet.Application.Services;

public class TPacienteService : ITPacienteService
{
    private readonly ITPacienteRepository _tPacienteRepository;
    private readonly IAppLogger<TPacienteService> _appLogger;

    public TPacienteService(ITPacienteRepository tPacienteRepository, IAppLogger<TPacienteService> appLogger)
    {
        _tPacienteRepository = tPacienteRepository;
        _appLogger = appLogger;
    }

    public async Task<IEnumerable<TPacienteDTO>> GetPacienteDTOsAsync()
    {
        var paciente = await _tPacienteRepository.GetPacienteAsync();

        _appLogger.LogInformation("Se recuperó la lista completa de Paciente.");

        return paciente.Select(e => new TPacienteDTO
        {
            PacienteID = e.NPacienteID,
            PersonaFK = e.NPersonaFK,
            GrupoSanguineoFK = e.NGrupoSanguineoFK,
            AlergiasGenerales = e.CAlergiasGenerales
        });
    }

    public async Task<TPacienteDTO?> GetPacienteIdDTOsAsync(int id)
    {
        var paciente = await _tPacienteRepository.GetPacienteIdAsync(id);

        if (paciente == null)
        {
            _appLogger.LogError("No se encontró un paciente con el ID {id}.", id);
            return null;
        }
        
        _appLogger.LogInformation("Paciente con ID {PacienteId} recuperado correctamente.", paciente.NPacienteID);

        return new TPacienteDTO
        {
            PacienteID = paciente.NPacienteID,
            PersonaFK = paciente.NPersonaFK,
            GrupoSanguineoFK = paciente.NGrupoSanguineoFK,
            AlergiasGenerales = paciente.CAlergiasGenerales
        };
    }

    public async Task CrearAsync(TPacienteDTO pacienteDTO)
    {
        var paciente = new TPaciente
        {
            NPersonaFK = pacienteDTO.PersonaFK,
            NGrupoSanguineoFK = pacienteDTO.GrupoSanguineoFK,
            CAlergiasGenerales = pacienteDTO.AlergiasGenerales
        };

        await _tPacienteRepository.AddAsync(paciente);
        await _tPacienteRepository.SaveChangeAsync();

        _appLogger.LogInformation("Paciente con ID {PacienteId} creado exitosamente.", paciente.NPacienteID);
    }

    public async Task ActualizarAsync(int id, TPacienteDTO pacienteDTO)
    {
        var paciente = await _tPacienteRepository.GetPacienteIdAsync(id);

        if (paciente == null)
        {
            _appLogger.LogError("Error al actualizar el paciente con ID {id}: no existe en el sistema.", id);
            return;
        }

        paciente.NPersonaFK = pacienteDTO.PersonaFK;
        paciente.NGrupoSanguineoFK = pacienteDTO.GrupoSanguineoFK;
        paciente.CAlergiasGenerales = pacienteDTO.AlergiasGenerales;

        _tPacienteRepository.Update(paciente);
        await _tPacienteRepository.SaveChangeAsync();

        _appLogger.LogInformation("Paciente con ID {PacienteId} actualizado correctamente.", paciente.NPacienteID);
    }

    public async Task EliminarAsync(int id)
    {
        var paciente = await _tPacienteRepository.GetPacienteIdAsync(id);

        if (paciente == null)
        {
            _appLogger.LogError("Error al eliminar el paciente con ID {id}: no existe en el sistema.", id);
            return;
        }

        _tPacienteRepository.Delete(paciente);
        await _tPacienteRepository.SaveChangeAsync();
        
        _appLogger.LogInformation("Paciente con ID {PacienteId} eliminado correctamente.", paciente.NPacienteID);
    }
}