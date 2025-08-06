using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Domain.interfaces;
using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Application.Services;

public class TEstadoVerificacionService : ITEstadoVerificacionService
{
    private readonly ITEstadoVerificacionRepository _tEstadoVerificacionRepository;

    public TEstadoVerificacionService(ITEstadoVerificacionRepository tEstadoVerificacionRepository)
    {
        _tEstadoVerificacionRepository = tEstadoVerificacionRepository;
    }

    public async Task<IEnumerable<TEstadoVerificacionDTO>> GetEstadoVerificacionDTOsAsync()
    {
        var estadoVerificacion = await _tEstadoVerificacionRepository.GetEstadoVerificacionAsync();

        return estadoVerificacion.Select(u => new TEstadoVerificacionDTO
        {
            EstadoVerificacionID = u.NEstadoVerificacionID,
            Nombre = u.CNombre
        });
    }
    public async Task<TEstadoVerificacionDTO?> GetEstadoVerificacionIdDTOsAsync(int id)
    {
        var estadoVerificacion = await _tEstadoVerificacionRepository.GetEstadoVerificacionIdAsync(id);

        if (estadoVerificacion == null) return null;

        return new TEstadoVerificacionDTO
        {
            EstadoVerificacionID = estadoVerificacion.NEstadoVerificacionID,
            Nombre = estadoVerificacion.CNombre
        };
    }
    public async Task CrearAsync(TEstadoVerificacionDTO DTOs)
    {
        var estadoVerificacion = new TEstadoVerificacion
        {
            CNombre = DTOs.Nombre
        };

        await _tEstadoVerificacionRepository.AddAsync(estadoVerificacion);
        await _tEstadoVerificacionRepository.SaveChangeAsync();
    }
    public async Task ActualizarAsync(int id, TEstadoVerificacionDTO DTOs)
    {
        var estadoVerificacion = await _tEstadoVerificacionRepository.GetEstadoVerificacionIdAsync(id);

        if (estadoVerificacion == null) return;

        estadoVerificacion.CNombre = DTOs.Nombre;

        _tEstadoVerificacionRepository.Update(estadoVerificacion);

        await _tEstadoVerificacionRepository.SaveChangeAsync();
    }
    public async Task EliminarAsync(int id)
    {
        var estadoVerificacion = await _tEstadoVerificacionRepository.GetEstadoVerificacionIdAsync(id);

        if (estadoVerificacion == null) return;

        _tEstadoVerificacionRepository.Delete(estadoVerificacion);

        await _tEstadoVerificacionRepository.SaveChangeAsync();
    }
}
