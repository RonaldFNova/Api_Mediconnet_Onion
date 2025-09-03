using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Domain.interfaces;

public interface ITEstadoCitaRepository
{
    Task<IEnumerable<TEstadoCita>> GetEstadoCitaAsync();
    Task<TEstadoCita?> GetEstadoCitaIdAsync(int id);
    Task AddAsync(TEstadoCita estadoCita);
    void Update(TEstadoCita estadoCita);
    void Delete(TEstadoCita estadoCita);
    Task<bool> SaveChangeAsync();
}