using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.interfaces;

public interface ITUsuariosService
{
    Task<IEnumerable<TUsuarioDTO>> GetUsuariosAsync();
    Task<TUsuarioDTO?> GetUsuariosIdAsync(int id);
    Task CrearAsync(TUsuarioDTO usuarioDto);
    Task ActualizarAsync(int id, TUsuarioDTO usuariosDto);
    Task EliminarAsync(int id);
}
