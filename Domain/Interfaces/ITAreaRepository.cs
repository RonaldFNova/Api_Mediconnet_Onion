using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Domain.Interfaces;

public interface ITAreaRepository
{
    Task<IEnumerable<TArea>> GetAreaAsync();
    Task<TArea?> GetAreaIdAsync(int id);
    Task AddAsync(TArea area);
    void Update(TArea area);
    void Delete(TArea area);
    Task<bool> SaveChangeAsync();
}