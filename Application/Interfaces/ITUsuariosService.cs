using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.interfaces;

public interface ITUsuariosService
{
    Task<IEnumerable<TUsuarioResponseDTO>> GetUsuariosAsync();

    Task<TUsuarioResponseDTO?> GetUsuariosIdAsync(int id);

    Task<string> CrearAsync(TUsuarioCreateDTO usuarioDto);

    Task ActualizarAsync(int id, TUsuarioCreateDTO usuariosDto);
    
    Task EliminarAsync(int id);
}