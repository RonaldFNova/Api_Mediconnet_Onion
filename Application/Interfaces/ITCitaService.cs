using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.Interfaces;
public interface ITCitaService
{
    Task<IEnumerable<TCitaDTO>> GetCitaDTOsAsync();
    Task<TCitaDTO?> GetCitaIdDTOsAsync(int id);
    Task CrearAsync(TCitaDTO citaDTO);
    Task ActualizarAsync(int id, TCitaDTO citaDTO);
    Task EliminarAsync(int id);
}