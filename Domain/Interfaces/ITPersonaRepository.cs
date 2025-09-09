using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Domain.Interfaces;

public interface ITPersonaRepository
{
    Task<IEnumerable<TPersona>> GetPersonaAsync();
    Task<TPersona?> GetPersonaIdAsync(int id);
    Task AddAsync(TPersona persona);
    void Update(TPersona persona);
    void Delete(TPersona persona);
    Task<bool> SaveChangeAsync();
}