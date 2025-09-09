using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.Interfaces;

public interface ITDiaSemanaServices
{
    Task<IEnumerable<TDiaSemanaDTO>> GetDiaSemanaDTOsAsync();
    Task<TDiaSemanaDTO?> GetDiaSemanaIdDTOsAsync(int id);
    Task CrearAsync(TDiaSemanaDTO diaSemanaDTO);
    Task ActualizarAsync(int id, TDiaSemanaDTO diaSemanaDTO);
    Task EliminarAsync(int id);
} 