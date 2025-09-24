using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.Interfaces;

public interface ITUsuarioService
{
    Task<IEnumerable<TUsuarioResponseDTO>> GetUsuarioAsync();
    Task<TUsuarioResponseDTO?> GetUsuarioIdAsync(int id);
    Task CrearAsync(TUsuarioCreateDTO usuarioDto);
    Task ActualizarAsync(int id, TUsuarioCreateDTO UsuarioDto);
    Task EliminarAsync(int id);
    Task<UsuarioEmailDTO> GetEmailAsync(int id);
    Task<UsuarioEmailDTO?> GetUsuarioEmailAsync(string email);
}