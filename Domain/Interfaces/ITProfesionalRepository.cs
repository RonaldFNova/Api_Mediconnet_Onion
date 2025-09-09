namespace Api_Mediconnet.Domain.Interfaces;

using Api_Mediconnet.Domain.Entities;

public interface ITProfesionalRepository
{
    Task<IEnumerable<TProfesional>> GetProfesionalesAsync();
    Task<TProfesional?> GetProfesionalIdAsync(int id);
    Task AddAsync(TProfesional profesional);
    void Update(TProfesional profesional);
    void Delete(TProfesional profesional);
    Task<bool> SaveChangeAsync();
}