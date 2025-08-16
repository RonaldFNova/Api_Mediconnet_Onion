using System.IO.Compression;
using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;

namespace Api_Mediconnet.Application.Services;

public class TDiaSemanaServices : ITDiaSemanaServices
{
    private readonly ITDiaSemanaRepository _tDiaSemanaRepository;

    public TDiaSemanaServices(ITDiaSemanaRepository tDiaSemanaRepository)
    {
        _tDiaSemanaRepository = tDiaSemanaRepository;
    }

    public async Task<IEnumerable<TDiaSemanaDTO>> GetDiaSemanaDTOsAsync()
    {
        var diaSemana = await _tDiaSemanaRepository.GetDiaSemanaAsync();

        return diaSemana.Select(u => new TDiaSemanaDTO
        {
            DiaSemanaID = u.NDiaSemanaID,
            Nombre = u.CNombre
        });
    }

    public async Task<TDiaSemanaDTO?> GetDiaSemanaIdDTOsAsync(int id)
    {
        var diaSemana = await _tDiaSemanaRepository.GetDiaSemanaIdAsync(id);

        if (diaSemana == null) return null;

        return new TDiaSemanaDTO
        {
            DiaSemanaID = diaSemana.NDiaSemanaID,
            Nombre = diaSemana.CNombre
        };
    }

    public async Task CrearAsync(TDiaSemanaDTO diaSemanaDTO)
    {
        var diaSemana = new TDiaSemana
        {
            CNombre = diaSemanaDTO.Nombre
        };

        await _tDiaSemanaRepository.AddAsync(diaSemana);
        await _tDiaSemanaRepository.SaveChangeAsync();
    }

    public async Task ActualizarAsync(int id, TDiaSemanaDTO diaSemanaDTO)
    {
        var diaSemana = await _tDiaSemanaRepository.GetDiaSemanaIdAsync(id);

        if (diaSemana == null) return;

        diaSemana.CNombre = diaSemanaDTO.Nombre;

        _tDiaSemanaRepository.Update(diaSemana);
        await _tDiaSemanaRepository.SaveChangeAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var diaSemana = await _tDiaSemanaRepository.GetDiaSemanaIdAsync(id);

        if (diaSemana == null) return;

        _tDiaSemanaRepository.Delete(diaSemana);
        await _tDiaSemanaRepository.SaveChangeAsync();
    }

}