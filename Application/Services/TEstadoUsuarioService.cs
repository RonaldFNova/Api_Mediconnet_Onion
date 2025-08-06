using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;

namespace Api_Mediconnet.Application.Services;

public class TEstadoUsuarioService : ITEstadoUsuarioService
{
    private readonly ITEstadoUsuarioRepository _tEstadoUsuarioRepository;

    public TEstadoUsuarioService(ITEstadoUsuarioRepository tEstadoUsuarioRepository)
    {
        _tEstadoUsuarioRepository = tEstadoUsuarioRepository;
    }

    public async Task<IEnumerable<TEstadoUsuarioDTO>> GetEstadoUsuarioDTOsAsync()
    {
        var estadoUsuario = await _tEstadoUsuarioRepository.GetEstadoUsuarioAsync();
        return estadoUsuario.Select(u => new TEstadoUsuarioDTO
        {
            EstadoUsuarioID = u.NEstadoUsuarioID,
            Estado = u.CNombre
        });
    }
    public async Task<TEstadoUsuarioDTO?> GetEstadoUsuarioIdDTOsAsync(int id)
    {
        var estadoUsuario = await _tEstadoUsuarioRepository.GetEstadoUsuarioIdAsync(id);

        if (estadoUsuario == null) return null;

        return new TEstadoUsuarioDTO
        {
            EstadoUsuarioID = estadoUsuario.NEstadoUsuarioID,
            Estado = estadoUsuario.CNombre
        };
    }
    public async Task CrearAsync(TEstadoUsuarioDTO DTOs)
    {
        var estadoUsuario = new TEstadoUsuario
        {
            CNombre = DTOs.Estado
        };

        await _tEstadoUsuarioRepository.AddAsync(estadoUsuario);
        await _tEstadoUsuarioRepository.SaveChangeAsync();
    }

    public async Task ActualizarAsync(int id, TEstadoUsuarioDTO DTOs)
    {
        var estadoUsuario = await _tEstadoUsuarioRepository.GetEstadoUsuarioIdAsync(id);

        if (estadoUsuario == null) return;

        estadoUsuario.CNombre = DTOs.Estado;

        _tEstadoUsuarioRepository.Update(estadoUsuario);
        await _tEstadoUsuarioRepository.SaveChangeAsync();
    }
    public async Task EliminarAsync(int id)
    {
        var estadoUsuario = await _tEstadoUsuarioRepository.GetEstadoUsuarioIdAsync(id);

        if (estadoUsuario == null) return;

        _tEstadoUsuarioRepository.Delete(estadoUsuario);
        await _tEstadoUsuarioRepository.SaveChangeAsync();

    }

}