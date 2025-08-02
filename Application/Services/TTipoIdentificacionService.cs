using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Domain.interfaces;
using Api_Mediconnet.Domain.Entities;


namespace Api_Mediconnet.Application.Services;

public class TTipoIdentificacionService : ITipoIdentificacionService
{
    private readonly ITTipoIdentificacionRepository _tTipoIdentificacionRepository;

    public TTipoIdentificacionService(ITTipoIdentificacionRepository tTipoIdentificacionRepository)
    {
        _tTipoIdentificacionRepository = tTipoIdentificacionRepository;
    }
    public async Task<IEnumerable<TTipoIdentificacionDTO>> GetTipoIdentificacionDTOsAsync()
    {
        var TipoIdentificacion = await _tTipoIdentificacionRepository.GetTipoIdentificacionAsync();

        return TipoIdentificacion.Select(u => new TTipoIdentificacionDTO
        {
            NTipoIdentificacionID = u.NTipoIdentificacionID,
            CNombre = u.CNombre
        });
    }
    public async Task<TTipoIdentificacionDTO?> GetTipoIdentificacionIdDTOsAsync(int id)
    {
        var TipoIdentificacion = await _tTipoIdentificacionRepository.GetTipoIdentificacionIdAsync(id);

        if (TipoIdentificacion == null) return null;

        return new TTipoIdentificacionDTO
        {
            NTipoIdentificacionID = TipoIdentificacion.NTipoIdentificacionID,
            CNombre = TipoIdentificacion.CNombre
        };
    }
    public async Task CrearAsync(TTipoIdentificacionDTO DTO)
    {
        var TipoIdentificacion = new TTipoIdentificacion
        {
            CNombre = DTO.CNombre
        };

        await _tTipoIdentificacionRepository.AddAsync(TipoIdentificacion);
        await _tTipoIdentificacionRepository.SaveChangeAsync();
    }
    public async Task ActualizarAsync(int id, TTipoIdentificacionDTO DTO)
    {
        var TipoIdentificacion = await _tTipoIdentificacionRepository.GetTipoIdentificacionIdAsync(id);

        if (TipoIdentificacion == null) return;

        TipoIdentificacion.CNombre = DTO.CNombre;

        _tTipoIdentificacionRepository.Update(TipoIdentificacion);
        await _tTipoIdentificacionRepository.SaveChangeAsync();
    }
    public async Task EliminarAsync(int id)
    {
        var TipoIdentificacion = await _tTipoIdentificacionRepository.GetTipoIdentificacionIdAsync(id);

        if (TipoIdentificacion == null) return;

        _tTipoIdentificacionRepository.Delete(TipoIdentificacion);
        await _tTipoIdentificacionRepository.SaveChangeAsync();
    }
}
