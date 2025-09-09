using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.Interfaces;

public interface ITEstadoCitaService
{
    Task<IEnumerable<TEstadoCitaDTO>> GetEstadoCitaDTOsAsync();
    Task<TEstadoCitaDTO?> GetEstadoCitaIdDTOsAsync(int id);
    Task CreateAsync(TEstadoCitaDTO estadoCitaDto);
    Task ActualizarAsync(int id, TEstadoCitaDTO estadoCitaDto);
    Task EliminarAsync(int id);
}