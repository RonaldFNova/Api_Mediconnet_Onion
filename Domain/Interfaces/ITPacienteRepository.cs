using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Domain.Interfaces;

public interface ITPacienteRepository
{
    Task<IEnumerable<TPaciente>> GetPacienteAsync();
    Task<TPaciente?> GetPacienteIdAsync(int id);
    Task AddAsync(TPaciente paciente);
    void Update(TPaciente paciente);
    void Delete(TPaciente paciente);
    Task<bool> SaveChangeAsync();
}