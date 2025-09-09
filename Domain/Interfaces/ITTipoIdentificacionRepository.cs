using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Domain.Interfaces;

public interface ITTipoIdentificacionRepository
{
    Task<IEnumerable<TTipoIdentificacion>> GetTipoIdentificacionAsync();
    Task<TTipoIdentificacion?> GetTipoIdentificacionIdAsync(int id);
    Task AddAsync(TTipoIdentificacion tipoIdentificacion);
    void Update(TTipoIdentificacion tipoIdentificacion);
    void Delete(TTipoIdentificacion tipoIdentificacion);
    Task<bool> SaveChangeAsync();
}