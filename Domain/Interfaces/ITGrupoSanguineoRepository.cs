using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Domain.interfaces;
public interface ITGrupoSanguineoRepository
{
    Task<IEnumerable<TGrupoSanguineo>> GetGrupoSanguineoAsync();
    Task<TGrupoSanguineo?> GetGrupoSanguineoIdAsync(int id);
    Task AddAsync(TGrupoSanguineo grupoSanguineo);
    void Update(TGrupoSanguineo grupoSanguineo);
    void Delete(TGrupoSanguineo grupoSanguineo);
    Task<bool> SaveChangeAsync();
}