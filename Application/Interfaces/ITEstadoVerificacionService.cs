using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.Interfaces;

public interface ITEstadoVerificacionService
{
    Task<IEnumerable<TEstadoVerificacionDTO>> GetEstadoVerificacionDTOsAsync();
    Task<TEstadoVerificacionDTO?> GetEstadoVerificacionIdDTOsAsync(int id);
    Task CrearAsync(TEstadoVerificacionDTO estadoVerificacionDTO);
    Task ActualizarAsync(int id, TEstadoVerificacionDTO estadoVerificacionDTO);
    Task EliminarAsync(int id);
}
