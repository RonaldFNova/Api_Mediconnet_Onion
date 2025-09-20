using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.Interfaces; 
using Api_Mediconnet.Domain.Enums;


namespace Api_Mediconnet.Application.Services;

public class TProfesionalService : ITProfesionalService
{
    private readonly ITProfesionalRepository _tProfesionalRepository;
    private readonly IAppLogger<TProfesionalService> _appLogger;

    public TProfesionalService(ITProfesionalRepository tProfesionalRepository, IAppLogger<TProfesionalService> appLogger)
    {
        _tProfesionalRepository = tProfesionalRepository;
        _appLogger = appLogger;
    }

    public async Task<IEnumerable<TProfesionalDTO>> GetProfesionalDTOsAsync()
    {
        var profesionales = await _tProfesionalRepository.GetProfesionalesAsync();

        _appLogger.LogInformation("Se recuperó la lista completa de Profesionales.");

        return profesionales.Select(u => new TProfesionalDTO
        {
            ProfesionalID = u.NProfesionalID,
            PersonaFK = u.NPersonaFK,
            RegistroProfesional = u.CRegistroProfesional,
            FechaContratacion = u.DFechaContratacion,
            TipoProfesional = u.ETipoProfesional.ToString(),
            Biografia = u.CBiografia
        });
    }

    public async Task<TProfesionalDTO?> GetProfesionalIdDTOsAsync(int id)
    {
        var profesional = await _tProfesionalRepository.GetProfesionalIdAsync(id);

        if (profesional == null)
        {
            _appLogger.LogError("No se encontró un profesional con el ID {id}.", id);
            return null;
        }

        _appLogger.LogInformation("Profesional con ID {ProfesionalId} recuperado correctamente.", profesional.NProfesionalID);

        return new TProfesionalDTO
        {
            ProfesionalID = profesional.NProfesionalID,
            PersonaFK = profesional.NPersonaFK,
            RegistroProfesional = profesional.CRegistroProfesional,
            FechaContratacion = profesional.DFechaContratacion,
            TipoProfesional = profesional.ETipoProfesional.ToString(),
            Biografia = profesional.CBiografia
        };
    }

    public async Task CrearAsync(TProfesionalDTO profesionalDto)
    {
        var profesional = new TProfesional
        {
            NPersonaFK = profesionalDto.PersonaFK,
            CRegistroProfesional = profesionalDto.RegistroProfesional,
            DFechaContratacion = profesionalDto.FechaContratacion,
            ETipoProfesional = Enum.Parse<Profesional>(profesionalDto.TipoProfesional),
            CBiografia = profesionalDto.Biografia
        };

        await _tProfesionalRepository.AddAsync(profesional);
        await _tProfesionalRepository.SaveChangeAsync();
        _appLogger.LogInformation("Profesional con ID {ProfesionalId} creado exitosamente.", profesional.NProfesionalID);
    }

    public async Task ActualizarAsync(int id, TProfesionalDTO profesionalDto)
    {
        var profesional = await _tProfesionalRepository.GetProfesionalIdAsync(id);

        if (profesional == null)
        {
            _appLogger.LogError("No se encontró un profesional con el ID {id} para actualizar.", id);
            return;
        }

        profesional.NPersonaFK = profesionalDto.PersonaFK;
        profesional.CRegistroProfesional = profesionalDto.RegistroProfesional;
        profesional.DFechaContratacion = profesionalDto.FechaContratacion;
        profesional.ETipoProfesional = Enum.Parse<Profesional>(profesionalDto.TipoProfesional);
        profesional.CBiografia = profesionalDto.Biografia;

        _tProfesionalRepository.Update(profesional);
        await _tProfesionalRepository.SaveChangeAsync();

        _appLogger.LogInformation("Profesional con ID {ProfesionalId} actualizado exitosamente.", profesional.NProfesionalID);
    }

    public async Task EliminarAsync(int id)
    {
        var profesional = await _tProfesionalRepository.GetProfesionalIdAsync(id);

        if (profesional == null)
        {
            _appLogger.LogError("No se encontró un profesional con el ID {id} para eliminar.", id);
            return;
        }

        _tProfesionalRepository.Delete(profesional);
        await _tProfesionalRepository.SaveChangeAsync();

        _appLogger.LogInformation("Profesional con ID {ProfesionalId} eliminado exitosamente.", profesional.NProfesionalID);
    }
}
