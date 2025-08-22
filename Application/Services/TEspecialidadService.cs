using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;

namespace Api_Mediconnet.Application.Services;

public class TEspecialidadService : ITEspecialidadService
{
    private readonly ITEspecialidadRepository _tEspecialidadRepository;


    public TEspecialidadService(ITEspecialidadRepository tEspecialidadRepository)
    {
        _tEspecialidadRepository = tEspecialidadRepository;
    }


    public async Task<IEnumerable<TEspecialidadDTO>> GetEspecialidadDTOsAsync()
    {
        var especialidad = await _tEspecialidadRepository.GetEspecialidadAsync();

        return especialidad.Select(e => new TEspecialidadDTO
        {
            EspecialidadID = e.NEspecialidadID,
            Nombre = e.CNombre,
            Descripcion = e.CDescripcion
        });
    }

    public async Task<TEspecialidadDTO?> GetEspecialidadIdDTOsAsync(int id)
    {
        var especialidad = await _tEspecialidadRepository.GetEspecialidadIdAsync(id);

        if (especialidad == null) return null;

        return new TEspecialidadDTO
        {
            EspecialidadID = especialidad.NEspecialidadID,
            Nombre = especialidad.CNombre,
            Descripcion = especialidad.CDescripcion
        };
    }

    public async Task CrearAsync(TEspecialidadDTO especialidadDTO)
    {
        var especialidad = new TEspecialidad
        {
            CNombre = especialidadDTO.Nombre,
            CDescripcion = especialidadDTO.Descripcion
        };

        await _tEspecialidadRepository.AddAsync(especialidad);
        await _tEspecialidadRepository.SaveChangesAsync();
    }

    public async Task ActualizarAsync(int id, TEspecialidadDTO especialidadDTO)
    {
        var especialidad = await _tEspecialidadRepository.GetEspecialidadIdAsync(id);

        if (especialidad == null) return;

        especialidad.CNombre = especialidadDTO.Nombre;
        especialidad.CDescripcion = especialidadDTO.Descripcion;

        _tEspecialidadRepository.Update(especialidad);
        await _tEspecialidadRepository.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var especialidad = await _tEspecialidadRepository.GetEspecialidadIdAsync(id);

        if (especialidad == null) return;

        _tEspecialidadRepository.Delete(especialidad);
        await _tEspecialidadRepository.SaveChangesAsync();
    }
}