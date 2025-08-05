using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Domain.interfaces;

public interface ITUsuariosRepository
{
    Task<IEnumerable<TUsuarios>> GetUsuariosAsync();
    Task<TUsuarios?> GetUsuariosIdAsync(int id);
    Task<string?> GetRolNombreByUsuarioIdAsync(int id);
    Task AddAsync(TUsuarios usuarios);
    void Update(TUsuarios usuarios);
    void Delete(TUsuarios usuarios);
    Task<bool> SaveChangesAsync();
}