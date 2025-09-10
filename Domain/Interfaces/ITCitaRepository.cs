namespace Api_Mediconnet.Domain.Interfaces;

using Api_Mediconnet.Domain.Entities;

public interface ITCitaRepository
{
    Task<IEnumerable<TCita>> GetCitaAsync();
    Task<TCita?> GetCitaIdAsync(int id);
    Task AddAsync(TCita cita);
    void Update(TCita cita);
    void Delete(TCita cita);
    Task<bool> SaveChangeAsync();
}