using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Domain.Interfaces;

public interface ITUsuarioRepository
{
    Task<IEnumerable<TUsuario>> GetUsuarioAsync();
    Task<TUsuario?> GetUsuarioIdAsync(int id);
    Task<string?> GetRolNombreByUsuarioIdAsync(int id);
    Task AddAsync(TUsuario Usuario);
    void Update(TUsuario Usuario);
    void Delete(TUsuario Usuario);
    Task<bool> SaveChangesAsync();
}