using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Domain.interfaces;

public interface ITRolRepository
{
    Task<IEnumerable<TRol>> GetRolAsync();
    Task<TRol?> GetRolIdAsync(int id);
    Task AddAsync(TRol rol);
    void Update(TRol rol);
    void Delete(TRol rol);
    Task<bool> SaveChangeAsync();
}