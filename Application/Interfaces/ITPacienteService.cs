using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.Interfaces;

public interface ITPacienteService
{
    Task<IEnumerable<TPacienteDTO>> GetPacienteDTOsAsync();
    Task<TPacienteDTO?> GetPacienteIdDTOsAsync(int id);
    Task CrearAsync(TPacienteDTO pacienteDTO);
    Task ActualizarAsync(int id, TPacienteDTO pacienteDTO);
    Task EliminarAsync(int id);
}