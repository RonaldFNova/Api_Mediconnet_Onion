using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.Interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.Interfaces;

namespace Api_Mediconnet.Application.Services;

public class TRolService : ITRolService
{
    private readonly ITRolRepository _tRolRepository;

    private readonly IAppLogger<TRolService> _appLogger;

    public TRolService(ITRolRepository tRolRepository, IAppLogger<TRolService> appLogger)
    {
        _tRolRepository = tRolRepository;
        _appLogger = appLogger;
    }

    public async Task<IEnumerable<TRolDTO>> GetRolDTOsAsync()
    {
        var rol = await _tRolRepository.GetRolAsync();

        _appLogger.LogInformation("Se recuperó la lista completa de Roles.");

        return rol.Select(e => new TRolDTO
        {
            RolID = e.NRolID,
            Rol = e.CNombre
        });
    }
    
    public async Task<TRolDTO?> GetRolIdAsync(int id)
    {
        var rol = await _tRolRepository.GetRolIdAsync(id);

        if (rol == null)
        {
            _appLogger.LogError("No se encontró un rol con el ID {id}.", id);
            return null;
        }

        _appLogger.LogInformation("Rol con ID {RolId} recuperado correctamente.", rol.NRolID);

        return new TRolDTO
        {
            RolID = rol.NRolID,
            Rol = rol.CNombre
        };
    }

    public async Task CrearAsync(TRolDTO dTO)
    {
        var rol = new TRol
        {
            NRolID = dTO.RolID,
            CNombre = dTO.Rol
        };

        await _tRolRepository.AddAsync(rol);
        await _tRolRepository.SaveChangeAsync();
        
        _appLogger.LogInformation("Rol con ID {RolId} creado exitosamente.", rol.NRolID);
    }

    public async Task ActualizarAsync(int id, TRolDTO dTO)
    {
        var rol = await _tRolRepository.GetRolIdAsync(id);

        if (rol == null)
        {
            _appLogger.LogError("Error al actualizar el rol con ID {id}: no existe en el sistema.", id);
            return;
        }

        rol.CNombre = dTO.Rol;

        _tRolRepository.Update(rol);
        await _tRolRepository.SaveChangeAsync();

        _appLogger.LogInformation("Rol con ID {RolId} actualizado correctamente.", rol.NRolID);
    }

    public async Task EliminarAsync(int id)
    {
        var rol = await _tRolRepository.GetRolIdAsync(id);

        if (rol == null)
        {
            _appLogger.LogError("Error al eliminar el rol con ID {id}: no existe en el sistema.", id);
            return;
        }

        _tRolRepository.Delete(rol);
        await _tRolRepository.SaveChangeAsync();

        _appLogger.LogInformation("Rol con ID {RolId} eliminado correctamente.", rol.NRolID);
    }
}