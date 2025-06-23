using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.interfaces;

public interface ITEstadoUsuarioService
{
    Task<IEnumerable<TEstadoUsuarioDTO>> GetEstadoUsuarioDTOsAsync();

    Task<TEstadoUsuarioDTO?> GetEstadoUsuarioIdDTOsAsync(int id);

    Task CrearAsync(TEstadoUsuarioDTO estadoUsuarioDTO);

    Task ActualizarAsync(int id, TEstadoUsuarioDTO estadoUsuarioDTO);
    
    Task EliminarAsync(int id);
}