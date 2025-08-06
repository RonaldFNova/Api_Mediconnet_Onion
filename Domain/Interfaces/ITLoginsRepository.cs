using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Domain.interfaces;

public interface ITLoginsRepository
{
    Task<IEnumerable<TLogins>> GetLoginsAsync();
    Task<TLogins?> GetLoginsIdAsync(int id);
    Task AddAsync(TLogins logins);
    void Update(TLogins logins);
    void Delete(TLogins logins);
    Task<bool> SaveChangeAsync();
}