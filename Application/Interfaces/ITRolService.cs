using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.interfaces;

public interface ITRolService
{
    Task<IEnumerable<TRolDTO>> GetRolDTOsAsync();

    Task<TRolDTO?> GetRolIdAsync(int id);

    Task CrearAsync(TRolDTO rolDTO);

    Task ActualizarAsync(int id, TRolDTO rolDTO);

    Task EliminarAsync(int id);
}