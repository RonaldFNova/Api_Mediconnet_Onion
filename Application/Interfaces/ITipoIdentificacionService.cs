using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.interfaces;

public interface ITipoIdentificacionService
{
    Task<IEnumerable<TTipoIdentificacionDTO>> GetTipoIdentificacionDTOsAsync();
    Task<TTipoIdentificacionDTO?> GetTipoIdentificacionIdDTOsAsync(int id);
    Task CrearAsync(TTipoIdentificacionDTO tipoIdentificacionDTO);
    Task ActualizarAsync(int id, TTipoIdentificacionDTO tipoIdentificacionDTO);
    Task EliminarAsync(int id);
}

