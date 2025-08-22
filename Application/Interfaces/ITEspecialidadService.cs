using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.interfaces;

public interface ITEspecialidadService
{

    Task<IEnumerable<TEspecialidadDTO>> GetEspecialidadDTOsAsync();
    Task<TEspecialidadDTO?> GetEspecialidadIdDTOsAsync(int id);
    Task CrearAsync(TEspecialidadDTO especialidadDTO);
    Task ActualizarAsync(int id, TEspecialidadDTO especialidadDTO);
    Task EliminarAsync(int id);
}