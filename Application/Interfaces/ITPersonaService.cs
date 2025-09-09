using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.Interfaces;

public interface ITPersonaService
{
    Task<IEnumerable<TPersonaDTO>> GetPersonaDTOsAsync();
    Task<TPersonaDTO?> GetPersonaIdDTOsAsync(int id);
    Task CrearAsync(TPersonaDTO personaDTO);
    Task ActualizarAsync(int id, TPersonaDTO personaDTO);
    Task EliminarAsync(int id);
}