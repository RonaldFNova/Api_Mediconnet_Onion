using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.interfaces;

public interface ITProfesionalService
{
    Task<IEnumerable<TProfesionalDTO>> GetProfesionalDTOsAsync();
    Task<TProfesionalDTO?> GetProfesionalIdDTOsAsync(int id);
    Task CrearAsync(TProfesionalDTO profesionalDto);
    Task ActualizarAsync(int id, TProfesionalDTO profesionalDto);
    Task EliminarAsync(int id);
}