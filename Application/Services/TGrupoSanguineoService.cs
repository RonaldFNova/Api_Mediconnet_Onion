using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;

namespace Api_Mediconnet.Application.Services;

public class TGrupoSanguineoService : ITGrupoSanguineoService
{
    private readonly ITGrupoSanguineoRepository _tGrupoSanguineoRepository;

    public TGrupoSanguineoService(ITGrupoSanguineoRepository tGrupoSanguineoRepository)
    {
        _tGrupoSanguineoRepository = tGrupoSanguineoRepository;
    }

    public async Task<IEnumerable<TGrupoSanguineoDTO>> GetGrupoSanguineoDTOsAsync()
    {
        var grupoSanguineo = await _tGrupoSanguineoRepository.GetGrupoSanguineoAsync();

        return grupoSanguineo.Select(u => new TGrupoSanguineoDTO
        {
            GrupoSanguineoID = u.NGrupoSanguineoID,
            Nombre = u.CNombre
        });
    }

    public async Task<TGrupoSanguineoDTO?> GetGrupoSanguineoIdDTOsAsync(int id)
    {
        var grupoSanguineo = await _tGrupoSanguineoRepository.GetGrupoSanguineoIdAsync(id);

        if (grupoSanguineo == null) return null;

        return new TGrupoSanguineoDTO
        {
            GrupoSanguineoID = grupoSanguineo.NGrupoSanguineoID,
            Nombre = grupoSanguineo.CNombre
        };
    }

    public async Task CrearAsync(TGrupoSanguineoDTO DTOs)
    {
        var grupoSanguineo = new TGrupoSanguineo
        {
            CNombre = DTOs.Nombre
        };

        await _tGrupoSanguineoRepository.AddAsync(grupoSanguineo);
        await _tGrupoSanguineoRepository.SaveChangeAsync();
    }

    public async Task ActualizarAsync(int id, TGrupoSanguineoDTO DTOs)
    {
        var grupoSanguineo = await _tGrupoSanguineoRepository.GetGrupoSanguineoIdAsync(id);

        if (grupoSanguineo == null) return;

        grupoSanguineo.CNombre = DTOs.Nombre;

        _tGrupoSanguineoRepository.Update(grupoSanguineo);
        await _tGrupoSanguineoRepository.SaveChangeAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var grupoSanguineo = await _tGrupoSanguineoRepository.GetGrupoSanguineoIdAsync(id);

        if (grupoSanguineo == null) return;

        _tGrupoSanguineoRepository.Delete(grupoSanguineo);
        await _tGrupoSanguineoRepository.SaveChangeAsync();
    }
}