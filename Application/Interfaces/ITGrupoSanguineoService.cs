using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.interfaces;

public interface ITGrupoSanguineoService
{
    Task<IEnumerable<TGrupoSanguineoDTO>> GetGrupoSanguineoDTOsAsync();
    Task<TGrupoSanguineoDTO?> GetGrupoSanguineoIdDTOsAsync(int id);
    Task CrearAsync(TGrupoSanguineoDTO grupoSanguineoDTO);
    Task ActualizarAsync(int id, TGrupoSanguineoDTO grupoSanguineoDTO);
    Task EliminarAsync(int id);
}