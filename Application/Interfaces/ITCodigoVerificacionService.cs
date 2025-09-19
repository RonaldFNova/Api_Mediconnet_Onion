using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.Interfaces;

public interface ITCodigoVerificacionService
{
    Task<IEnumerable<TCodigoVerificacionDTO>> GetCodigoVerificacionDTOsAsync();
    Task<TCodigoVerificacionDTO?> GetCodigoVerificacionIdDTOsAsync(int id);
    Task CrearAsync(TCodigoVerificacionDTO codigoVerificacionDTO);
    Task ActualizarAsync(int id, TCodigoVerificacionDTO codigoVerificacionDTO);
    Task EliminarAsync(int id);
    Task<bool> ValidarCodigoVerificacionAsync(int id, string codigo);
}