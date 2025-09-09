using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.Interfaces;

public interface ITEstadoUsuarioervice
{
    Task<IEnumerable<TEstadoUsuarioDTO>> GetEstadoUsuarioDTOsAsync();

    Task<TEstadoUsuarioDTO?> GetEstadoUsuarioIdDTOsAsync(int id);

    Task CrearAsync(TEstadoUsuarioDTO estadoUsuarioDTO);

    Task ActualizarAsync(int id, TEstadoUsuarioDTO estadoUsuarioDTO);
    
    Task EliminarAsync(int id);
}