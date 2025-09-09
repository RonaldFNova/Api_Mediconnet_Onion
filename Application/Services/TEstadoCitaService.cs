using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.Interfaces;

namespace Api_Mediconnet.Application.Services;

public class TEstadoCitaService : ITEstadoCitaService
{
    private readonly ITEstadoCitaRepository _estadoCitaRepository;
    private readonly IAppLogger<TEstadoCitaService> _appLogger;

    public TEstadoCitaService(ITEstadoCitaRepository estadoCitaRepository, IAppLogger<TEstadoCitaService> appLogger)
    {
        _estadoCitaRepository = estadoCitaRepository;
        _appLogger = appLogger;
    }

    public async Task<IEnumerable<TEstadoCitaDTO>> GetEstadoCitaDTOsAsync()
    {
        var estadoCitas = await _estadoCitaRepository.GetEstadoCitaAsync();

        _appLogger.LogInformation("Se recuperó la lista completa de Estados de cita.");

        return estadoCitas.Select(e => new TEstadoCitaDTO
        {
            EstadoCitaID = e.NEstadoCitaID,
            Nombre = e.CNombre
        });
    }

    public async Task<TEstadoCitaDTO?> GetEstadoCitaIdDTOsAsync(int id)
    {
        var estadoCita = await _estadoCitaRepository.GetEstadoCitaIdAsync(id);

        if (estadoCita == null)
        {
            _appLogger.LogError("No se encontró un estado de cita con el ID {id}.", id);
            return null;
        };

        _appLogger.LogInformation("Estado de cita con ID {NEstadoCitaID} recuperado correctamente.", estadoCita.NEstadoCitaID);

        return new TEstadoCitaDTO
        {
            EstadoCitaID = estadoCita.NEstadoCitaID,
            Nombre = estadoCita.CNombre
        };
    }

    public async Task CreateAsync(TEstadoCitaDTO estadoCitaDto)
    {
        var estadoCita = new TEstadoCita
        {
            CNombre = estadoCitaDto.Nombre
        };

        await _estadoCitaRepository.AddAsync(estadoCita);
        await _estadoCitaRepository.SaveChangeAsync();

        _appLogger.LogInformation("Nuevo estado de cita creado con ID {NEstadoCitaID}.", estadoCita.NEstadoCitaID);
    }

    public async Task ActualizarAsync(int id, TEstadoCitaDTO estadoCitaDto)
    {
        var estadoCita = await _estadoCitaRepository.GetEstadoCitaIdAsync(id);

        if (estadoCita == null) 
        {
            _appLogger.LogError("No se encontró un estado de cita con el ID {id} para actualizar.", id);
            return;
        };

        estadoCita.CNombre = estadoCitaDto.Nombre;

        _estadoCitaRepository.Update(estadoCita);
        await _estadoCitaRepository.SaveChangeAsync();

        _appLogger.LogInformation("Estado de cita con ID {NEstadoCitaID} actualizado correctamente.", estadoCita.NEstadoCitaID);
    }

    public async Task EliminarAsync(int id)
    {
        var estadoCita = await _estadoCitaRepository.GetEstadoCitaIdAsync(id);

        if (estadoCita == null)
        {
            _appLogger.LogError("No se encontró un estado de cita con el ID {id} para eliminar.", id);
            return;
        };

        _estadoCitaRepository.Delete(estadoCita);
        await _estadoCitaRepository.SaveChangeAsync();

        _appLogger.LogInformation("Estado de cita con ID {NEstadoCitaID} eliminado correctamente.", estadoCita.NEstadoCitaID);
    }
}