using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Domain.Interfaces;
public interface ITEstadoVerificacionRepository
{
    Task<IEnumerable<TEstadoVerificacion>> GetEstadoVerificacionAsync();
    Task<TEstadoVerificacion?> GetEstadoVerificacionIdAsync(int id);
    Task AddAsync(TEstadoVerificacion estadoVerificacion);
    void Update(TEstadoVerificacion estadoVerificacion);
    void Delete(TEstadoVerificacion estadoVerificacion);
    Task<bool> SaveChangeAsync();
}