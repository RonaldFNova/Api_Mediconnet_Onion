using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Domain.interfaces;

public interface ITEstadoUsuarioRepository
{
    Task<IEnumerable<TEstadoUsuario>> GetEstadoUsuarioAsync();
    Task<TEstadoUsuario?> GetEstadoUsuarioIdAsync(int id);
    Task AddAsync(TEstadoUsuario estadoUsuario);
    void Update(TEstadoUsuario estadoUsuario);
    void Delete(TEstadoUsuario estadoUsuario);
    Task<bool> SaveChangeAsync();
}