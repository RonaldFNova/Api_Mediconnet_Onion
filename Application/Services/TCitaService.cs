using Api_Mediconnet.Application.Interfaces;
using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.Interfaces;

namespace Api_Mediconnet.Application.Services;

public class TCitaService : ITCitaService
{
    private readonly ITCitaRepository _tCitaRepository;
    private readonly IAppLogger<TCitaService> _logger;

    public TCitaService(ITCitaRepository tCitaRepository, IAppLogger<TCitaService> logger)
    {
        _tCitaRepository = tCitaRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<TCitaDTO>> GetCitaDTOsAsync()
    {
        var citas = await _tCitaRepository.GetCitaAsync();

        _logger.LogInformation("Se recuperó la lista completa de Citas.");

        return citas.Select(c => new TCitaDTO
        {
            NCitaID = c.NCitaID,
            EstadoCitaFK = c.NEstadoCitaFK,
            ProfesionalFK = c.NProfesionalFK,
            PacienteFK = c.NPacienteFK,
            Fecha = c.DFecha,
            Hora = c.DHora,
            Duracion = c.DDuracion,
            Observacion = c.CObservacion,
            FechaRegistro = c.DFechaRegistro
        });
    }

    public async Task<TCitaDTO?> GetCitaIdDTOsAsync(int id)
    {
        var cita = await _tCitaRepository.GetCitaIdAsync(id);

        if (cita == null)
        {
            _logger.LogError("No se encontró una cita con el ID {id}.", id);
            return null;
        }

        _logger.LogInformation("Cita con ID {NCitaID} recuperada correctamente.", cita.NCitaID);

        return new TCitaDTO
        {
            NCitaID = cita.NCitaID,
            EstadoCitaFK = cita.NEstadoCitaFK,
            ProfesionalFK = cita.NProfesionalFK,
            PacienteFK = cita.NPacienteFK,
            Fecha = cita.DFecha,
            Hora = cita.DHora,
            Duracion = cita.DDuracion,
            Observacion = cita.CObservacion,
            FechaRegistro = cita.DFechaRegistro
        };
    }

    public async Task CrearAsync(TCitaDTO citaDto)
    {
        var cita = new TCita
        {
            NEstadoCitaFK = citaDto.EstadoCitaFK,
            NProfesionalFK = citaDto.ProfesionalFK,
            NPacienteFK = citaDto.PacienteFK,
            DFecha = citaDto.Fecha,
            DHora = citaDto.Hora,
            DDuracion = citaDto.Duracion,
            CObservacion = citaDto.Observacion,
            DFechaRegistro = citaDto.FechaRegistro
        };

        await _tCitaRepository.AddAsync(cita);
        await _tCitaRepository.SaveChangeAsync();

        _logger.LogInformation("Cita con ID {NCitaID} creada correctamente.", cita.NCitaID);
    }

    public async Task ActualizarAsync(int id, TCitaDTO citaDto)
    {
        var cita = await _tCitaRepository.GetCitaIdAsync(id);

        if (cita == null)
        {
            _logger.LogError("No se encontró una cita con el ID {id}.", id);
            return;
        }

        cita.NEstadoCitaFK = citaDto.EstadoCitaFK;
        cita.NProfesionalFK = citaDto.ProfesionalFK;
        cita.NPacienteFK = citaDto.PacienteFK;
        cita.DFecha = citaDto.Fecha;
        cita.DHora = citaDto.Hora;
        cita.DDuracion = citaDto.Duracion;
        cita.CObservacion = citaDto.Observacion;
        cita.DFechaRegistro = citaDto.FechaRegistro;

        _tCitaRepository.Update(cita);
        await _tCitaRepository.SaveChangeAsync();

        _logger.LogInformation("Cita con ID {NCitaID} actualizada correctamente.", citaDto.NCitaID);
    }

    public async Task EliminarAsync(int id)
    {
        var cita = await _tCitaRepository.GetCitaIdAsync(id);

        if (cita == null)
        {
            _logger.LogError("No se encontró una cita con el ID {id}.", id);
            return; 
        }

        _tCitaRepository.Delete(cita);
        await _tCitaRepository.SaveChangeAsync();

        _logger.LogInformation("Cita con ID {NCitaID} eliminada correctamente.", id);
    }
}


