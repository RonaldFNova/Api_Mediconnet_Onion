using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Domain.Interfaces;

public interface ITLoginsRepository
{
    Task<IEnumerable<TLogins>> GetLoginsAsync();
    Task<TLogins?> GetLoginsIdAsync(int id);
    Task<TUsuario?> GetByEmailAsync(string email);
    Task AddAsync(TLogins logins);
    void Update(TLogins logins);
    void Delete(TLogins logins);
    Task<bool> SaveChangeAsync();
}