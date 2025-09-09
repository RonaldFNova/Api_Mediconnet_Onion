using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.Interfaces;

public interface ITProfesionalService
{
    Task<IEnumerable<TProfesionalDTO>> GetProfesionalDTOsAsync();
    Task<TProfesionalDTO?> GetProfesionalIdDTOsAsync(int id);
    Task CrearAsync(TProfesionalDTO profesionalDto);
    Task ActualizarAsync(int id, TProfesionalDTO profesionalDto);
    Task EliminarAsync(int id);
}