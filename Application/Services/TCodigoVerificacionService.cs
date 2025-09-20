using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.Interfaces;
using Api_Mediconnet.Domain.Enums;

namespace Api_Mediconnet.Application.Services;

public class TCodigoVerificacionService : ITCodigoVerificacionService
{
    private readonly ITCodigoVerificacionRepository _tCodigoVerificacionRepository;
    private readonly ITUsuarioRepository _tUsuarioRepository;
    private readonly IAppLogger<TCodigoVerificacionService> _appLogger;

    public TCodigoVerificacionService(ITCodigoVerificacionRepository tCodigoVerificacionRepository, IAppLogger<TCodigoVerificacionService> appLogger, ITUsuarioRepository tUsuarioRepository)
    {
        _tUsuarioRepository = tUsuarioRepository;
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

    public async Task<ValidarCodigoVerificacionResponseDTO> ValidarCodigoVerificacionAsync(int id, string codigo)
    {
        var codigoVerificacion = await _tCodigoVerificacionRepository.GetCodigoUserFkAsync(id);

        if (codigoVerificacion == null)
        {
            _appLogger.LogError("No se encontró un código de verificación con el ID {id} para validar.", id);
            return new ValidarCodigoVerificacionResponseDTO
            {
                StatusCode = 404,
                Mensaje = "El código no fue encontrado"
            };
        }

        if (codigoVerificacion.BUsado)
        {
            _appLogger.LogWarning("El código de verificación con ID {id} ya ha sido usado.", id);
            return new ValidarCodigoVerificacionResponseDTO
            {
                StatusCode = 409,
                Mensaje = "El código ya ha sido usado"
            };
        }

        if (DateTime.UtcNow > codigoVerificacion.DFechaExpiracion)
        {
            _appLogger.LogWarning("El código de verificación con ID {id} ha expirado.", id);
            codigoVerificacion.BUsado = true;

            _tCodigoVerificacionRepository.Update(codigoVerificacion);
            await _tCodigoVerificacionRepository.SaveChangesAsync();

            return new ValidarCodigoVerificacionResponseDTO
            {
                StatusCode = 410,
                Mensaje = "El código ya ha expirado"
            };
        }

        if (codigoVerificacion.CCodigo == codigo)
        {
            codigoVerificacion.BUsado = true;
            _appLogger.LogWarning("El código de verificación con ID {id} es correcto. Intento {Intentos}.", id, codigoVerificacion.NIntentos);

            _tCodigoVerificacionRepository.Update(codigoVerificacion);
            await _tCodigoVerificacionRepository.SaveChangesAsync();

            var usuario = await _tUsuarioRepository.GetUsuarioIdAsync(codigoVerificacion.NUsuarioFK);

            usuario.NEstadoVerificacionFK = 2;

            _tUsuarioRepository.Update(usuario);
            await _tUsuarioRepository.SaveChangesAsync();

            return new ValidarCodigoVerificacionResponseDTO
            {
                StatusCode = 200,
                Mensaje = "Código verificado correctamente"
            };
        }
        else
        {
            codigoVerificacion.NIntentos += 1;

            if (codigoVerificacion.NIntentos > 9)
            {
                codigoVerificacion.BUsado = true;
                _appLogger.LogWarning("El código de verificación con ID {id} ha sido bloqueado por exceder el número de intentos.", id);
            }

            _appLogger.LogWarning("El código de verificación con ID {id} es incorrecto.", id);

            _tCodigoVerificacionRepository.Update(codigoVerificacion);
            await _tCodigoVerificacionRepository.SaveChangesAsync();

            return new ValidarCodigoVerificacionResponseDTO
            {
                StatusCode = 400,
                Mensaje = "El código ingresado no es válido"
            };
        }
    }
    
}