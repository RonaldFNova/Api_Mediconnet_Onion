using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;

namespace Api_Mediconnet.Application.Services;

public class TPacienteService : ITPacienteService
{
    private readonly ITPacienteRepository _tPacienteRepository;

    public TPacienteService(ITPacienteRepository tPacienteRepository)
    {
        _tPacienteRepository = tPacienteRepository;
    }

    public async Task<IEnumerable<TPacienteDTO>> GetPacienteDTOsAsync()
    {
        var paciente = await _tPacienteRepository.GetPacienteAsync();

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

        if (paciente == null) return null;

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
    }

    public async Task ActualizarAsync(int id, TPacienteDTO pacienteDTO)
    {
        var paciente = await _tPacienteRepository.GetPacienteIdAsync(id);

        if (paciente == null) return;

        paciente.NPersonaFK = pacienteDTO.PersonaFK;
        paciente.NGrupoSanguineoFK = pacienteDTO.GrupoSanguineoFK;
        paciente.CAlergiasGenerales = pacienteDTO.AlergiasGenerales;

        _tPacienteRepository.Update(paciente);
        await _tPacienteRepository.SaveChangeAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var paciente = await _tPacienteRepository.GetPacienteIdAsync(id);

        if (paciente == null) return;

        _tPacienteRepository.Delete(paciente);
        await _tPacienteRepository.SaveChangeAsync();
    }
}