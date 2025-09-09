using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.Interfaces;

namespace Api_Mediconnet.Application.Services;

public class TAreaService : ITAreaService
{
    private readonly ITAreaRepository _tAreaRepository;
    private readonly IAppLogger<TAreaService> _logger;


    public TAreaService(ITAreaRepository tAreaRepository, IAppLogger<TAreaService> logger)
    {
        _tAreaRepository = tAreaRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<TAreaDTO>> GetAreaDTOsAsync()
    {
        var area = await _tAreaRepository.GetAreaAsync();

        _logger.LogInformation("Se recuperó la lista completa de Áreas.");

        return area.Select(a => new TAreaDTO
        {
            AreaID = a.NAreaID,
            Nombre = a.CNombre,
            Descripcion = a.CDescripcion
        });
    }

    public async Task<TAreaDTO?> GetAreaIdDTOsAsync(int id)
    {
        var area = await _tAreaRepository.GetAreaIdAsync(id);

        if (area == null)
        {
            _logger.LogError("No se encontró un área con el ID {id}.", id);
            return null;
        }

        _logger.LogInformation("Área con ID {NAreaID} recuperado correctamente.", area.NAreaID);

        return new TAreaDTO
        {
            AreaID = area.NAreaID,
            Nombre = area.CNombre,
            Descripcion = area.CDescripcion
        };
    }

    public async Task CrearAsync(TAreaDTO areaDto)
    {
        var area = new TArea
        {
            CNombre = areaDto.Nombre,
            CDescripcion = areaDto.Descripcion
        };

        await _tAreaRepository.AddAsync(area);
        await _tAreaRepository.SaveChangeAsync();

        _logger.LogInformation("Área con ID {NAreaID} creada correctamente.", areaDto.AreaID);
    }

    public async Task<TAreaDTO?> ActualizarAsync(int id, TAreaDTO areaDto)
    {
       var area = await _tAreaRepository.GetAreaIdAsync(id);

        if (area == null)
        {
            _logger.LogError("No se encontró un área con el ID {id}.", id);
            return null;
        }

        area.CNombre = areaDto.Nombre;
        area.CDescripcion = areaDto.Descripcion;

        _tAreaRepository.Update(area);
        await _tAreaRepository.SaveChangeAsync();

        _logger.LogInformation("Área con ID {NAreaID} actualizada correctamente.", areaDto.AreaID);

        return new TAreaDTO
        {
            AreaID = area.NAreaID,
            Nombre = area.CNombre,
            Descripcion = area.CDescripcion
        };
    }

    public async Task EliminarAsync(int id)
    {
        var area = await _tAreaRepository.GetAreaIdAsync(id);

        if (area == null)
        {
            _logger.LogError("No se encontró un área con el ID {id}.", id);
            return;
        }

        _tAreaRepository.Delete(area);
        await _tAreaRepository.SaveChangeAsync();

        _logger.LogInformation("Área con ID {NAreaID} eliminada correctamente.", area.NAreaID);
    }
}