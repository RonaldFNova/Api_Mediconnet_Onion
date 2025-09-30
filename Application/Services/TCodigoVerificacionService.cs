using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.Interfaces;
using Api_Mediconnet.Domain.Enums;

namespace Api_Mediconnet.Application.Services;

public class TCodigoVerificacionService : ITCodigoVerificacionService
{
    private readonly ITCodigoVerificacionRepository _tCodigoVerificacionRepository;
    private readonly IAppLogger<TCodigoVerificacionService> _appLogger;

    public TCodigoVerificacionService(ITCodigoVerificacionRepository tCodigoVerificacionRepository, IAppLogger<TCodigoVerificacionService> appLogger)
    {
        _tCodigoVerificacionRepository = tCodigoVerificacionRepository;
        _appLogger = appLogger;
    }

    public async Task<IEnumerable<TCodigoVerificacionDTO>> GetCodigoVerificacionDTOsAsync()
    {
        var codigoVerificacion = await _tCodigoVerificacionRepository.GetCodigoVerificacionAsync();

        _appLogger.LogInformation("Se recuperó la lista completa de Códigos de Verificación.");

        return codigoVerificacion.Select(c => new TCodigoVerificacionDTO
        {
            CodigoVerificacionID = c.NCodigoVerificacionID,
            Codigo = c.CCodigo,
            UsuarioFK = c.NUsuarioFK,
            FechaExpiracion = c.DFechaExpiracion,
            Usado = c.BUsado,
            FechaCreacion = c.DFechaCreacion
        });
    }

    public async Task<TCodigoVerificacionDTO?> GetCodigoVerificacionIdDTOsAsync(int id)
    {
        var codigoVerificacion = await _tCodigoVerificacionRepository.GetCodigoVerificacionIdAsync(id);

        if (codigoVerificacion == null)
        {
            _appLogger.LogError("No se encontró un código de verificación con el ID {id}.", id);
            return null;
        }

        _appLogger.LogInformation("Código de Verificación con ID {id} recuperado correctamente.", id);

        return new TCodigoVerificacionDTO
        {
            CodigoVerificacionID = codigoVerificacion.NCodigoVerificacionID,
            Codigo = codigoVerificacion.CCodigo,
            UsuarioFK = codigoVerificacion.NUsuarioFK,
            FechaExpiracion = codigoVerificacion.DFechaExpiracion,
            Usado = codigoVerificacion.BUsado,
            FechaCreacion = codigoVerificacion.DFechaCreacion
        };
    }

    public async Task CrearAsync(TCodigoVerificacionDTO codigoVerificacionDTO)
    {
        var codigoVerificacion = new TCodigoVerificacion
        {
            CCodigo = codigoVerificacionDTO.Codigo,
            NUsuarioFK = codigoVerificacionDTO.UsuarioFK,
            DFechaExpiracion = codigoVerificacionDTO.FechaExpiracion,
            BUsado = codigoVerificacionDTO.Usado,
            DFechaCreacion = DateTime.UtcNow,
            ETipoCodigo = Enum.Parse<TipoCodigoVerificacion>(codigoVerificacionDTO.TipoCodigo),
            NIntentos = codigoVerificacionDTO.Intentos
        };

        await _tCodigoVerificacionRepository.AddAsync(codigoVerificacion);
        await _tCodigoVerificacionRepository.SaveChangesAsync();

        _appLogger.LogInformation("Código de Verificación creado correctamente con ID {CodigoVerificacionID}.", codigoVerificacion.NCodigoVerificacionID);
    }

    public async Task ActualizarAsync(int id, TCodigoVerificacionDTO codigoVerificacionDTO)
    {
        var codigoVerificacion = await _tCodigoVerificacionRepository.GetCodigoVerificacionIdAsync(id);

        if (codigoVerificacion == null)
        {
            _appLogger.LogError("No se encontró un código de verificación con el ID {id} para actualizar.", id);
            return;
        }

        codigoVerificacion.CCodigo = codigoVerificacionDTO.Codigo;
        codigoVerificacion.NUsuarioFK = codigoVerificacionDTO.UsuarioFK;
        codigoVerificacion.DFechaExpiracion = codigoVerificacionDTO.FechaExpiracion;
        codigoVerificacion.BUsado = codigoVerificacionDTO.Usado;

        _tCodigoVerificacionRepository.Update(codigoVerificacion);
        await _tCodigoVerificacionRepository.SaveChangesAsync();

        _appLogger.LogInformation("Código de Verificación con ID {id} actualizado correctamente.", id);
    }

    public async Task EliminarAsync(int id)
    {
        var codigoVerificacion = await _tCodigoVerificacionRepository.GetCodigoVerificacionIdAsync(id);

        if (codigoVerificacion == null)
        {
            _appLogger.LogError("No se encontró un código de verificación con el ID {id} para eliminar.", id);
            return;
        }

        _tCodigoVerificacionRepository.Delete(codigoVerificacion);
        await _tCodigoVerificacionRepository.SaveChangesAsync();

        _appLogger.LogInformation("Código de Verificación con ID {id} eliminado correctamente.", id);
    }
    
}