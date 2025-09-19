using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Domain.Interfaces;

public interface ITCodigoVerificacionRepository
{
    Task<IEnumerable<TCodigoVerificacion>> GetCodigoVerificacionAsync();
    Task<TCodigoVerificacion?> GetCodigoVerificacionIdAsync(int id);
    Task AddAsync(TCodigoVerificacion codigoVerificacion);
    void Update(TCodigoVerificacion codigoVerificacion);
    void Delete(TCodigoVerificacion codigoVerificacion);
    Task<bool> SaveChangesAsync();
    Task<TCodigoVerificacion?> GetCodigoUserFkAsync(int id);
}