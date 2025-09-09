using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Domain.Interfaces;

public interface ITEspecialidadRepository
{
    Task<IEnumerable<TEspecialidad>> GetEspecialidadAsync();
    Task<TEspecialidad?> GetEspecialidadIdAsync(int id);
    Task AddAsync(TEspecialidad especialidad);
    void Update(TEspecialidad especialidad);
    void Delete(TEspecialidad especialidad);
    Task<bool> SaveChangesAsync();
}