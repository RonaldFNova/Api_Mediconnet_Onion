using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;

namespace Api_Mediconnet.Application.Services;

public class TRolService : ITRolService
{
    private readonly ITRolRepository _tRolRepository;

    public TRolService(ITRolRepository tRolRepository)
    {
        _tRolRepository = tRolRepository;
    }

    public async Task<IEnumerable<TRolDTO>> GetRolDTOsAsync()
    {
        var rol = await _tRolRepository.GetRolAsync();
        return rol.Select(e => new TRolDTO
        {
            NRolID = e.NRolID,
            CRol = e.CNombre
        });
    }
    public async Task<TRolDTO?> GetRolIdAsync(int id)
    {
        var rol = await _tRolRepository.GetRolIdAsync(id);

        if (rol == null) return null;

        return new TRolDTO
        {
            NRolID = rol.NRolID,
            CRol = rol.CNombre
        };
    }
    public async Task CrearAsync(TRolDTO dTO)
    {
        var Rol = new TRol
        {
            NRolID = dTO.NRolID,
            CNombre = dTO.CRol
        };

        await _tRolRepository.AddAsync(Rol);
        await _tRolRepository.SaveChangeAsync();
    }
    public async Task ActualizarAsync(int id, TRolDTO dTO)
    {
        var Rol = await _tRolRepository.GetRolIdAsync(id);

        if (Rol == null) return;

        Rol.CNombre = dTO.CRol;

        _tRolRepository.Update(Rol);
        await _tRolRepository.SaveChangeAsync();
    }
    public async Task EliminarAsync(int id)
    {
        var rol = await _tRolRepository.GetRolIdAsync(id);

        if (rol == null) return;

        _tRolRepository.Delete(rol);
        await _tRolRepository.SaveChangeAsync();

    }
}