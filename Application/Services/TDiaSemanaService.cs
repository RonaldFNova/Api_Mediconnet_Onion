using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;

namespace Api_Mediconnet.Application.Services;

public class TDiaSemanaService : ITDiaSemanaServices
{
    private readonly ITDiaSemanaRepository _tDiaSemanaRepository;
    private readonly IAppLogger<TDiaSemanaService> _appLogger;

    public TDiaSemanaService(ITDiaSemanaRepository tDiaSemanaRepository, IAppLogger<TDiaSemanaService> appLogger)
    {
        _tDiaSemanaRepository = tDiaSemanaRepository;
        _appLogger = appLogger;
    }

    public async Task<IEnumerable<TDiaSemanaDTO>> GetDiaSemanaDTOsAsync()
    {
        var diaSemana = await _tDiaSemanaRepository.GetDiaSemanaAsync();

        _appLogger.LogInformation("Se recuperó la lista completa de Días de la Semana.");

        return diaSemana.Select(u => new TDiaSemanaDTO
        {
            DiaSemanaID = u.NDiaSemanaID,
            Nombre = u.CNombre
        });
    }

    public async Task<TDiaSemanaDTO?> GetDiaSemanaIdDTOsAsync(int id)
    {
        var diaSemana = await _tDiaSemanaRepository.GetDiaSemanaIdAsync(id);

        if (diaSemana == null)
        {
            _appLogger.LogError("No se encontró un día de la semana con el ID {id}.", id);
            return null;
        }

        _appLogger.LogInformation("Día de la Semana con ID {DiaSemanaId} recuperado correctamente.", diaSemana.NDiaSemanaID);

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

        _appLogger.LogInformation("Día de la Semana con ID {DiaSemanaId} creado exitosamente.", diaSemana.NDiaSemanaID);
    }

    public async Task ActualizarAsync(int id, TDiaSemanaDTO diaSemanaDTO)
    {
        var diaSemana = await _tDiaSemanaRepository.GetDiaSemanaIdAsync(id);

        if (diaSemana == null)
        {
            _appLogger.LogError("No se encontró un día de la semana con el ID {id} para actualizar.", id);
            return;
        }

        diaSemana.CNombre = diaSemanaDTO.Nombre;

        _tDiaSemanaRepository.Update(diaSemana);
        await _tDiaSemanaRepository.SaveChangeAsync();

        _appLogger.LogInformation("Día de la Semana con ID {DiaSemanaId} actualizado correctamente.", diaSemana.NDiaSemanaID);
    }

    public async Task EliminarAsync(int id)
    {
        var diaSemana = await _tDiaSemanaRepository.GetDiaSemanaIdAsync(id);

        if (diaSemana == null)
        {
            _appLogger.LogError("No se encontró un día de la semana con el ID {id} para eliminar.", id);
            return;
        }

        _tDiaSemanaRepository.Delete(diaSemana);
        await _tDiaSemanaRepository.SaveChangeAsync();

        _appLogger.LogInformation("Día de la Semana con ID {DiaSemanaId} eliminado correctamente.", diaSemana.NDiaSemanaID);
    }

}