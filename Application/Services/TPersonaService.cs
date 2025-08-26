using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;

namespace Api_Mediconnet.Application.Services;

public class TPersonaService : ITPersonaService
{
    private readonly ITPersonaRepository _tPersonaRepository;
    private readonly IAppLogger<TPersonaService> _appLogger;

    public TPersonaService(ITPersonaRepository tPersonaRepository, IAppLogger<TPersonaService> appLogger)
    {
        _tPersonaRepository = tPersonaRepository;
        _appLogger = appLogger;
    }

    public async Task<IEnumerable<TPersonaDTO>> GetPersonaDTOsAsync()
    {
        var personaDTO = await _tPersonaRepository.GetPersonaAsync();

        _appLogger.LogInformation("Se recuperó la lista completa de Personas.");

        return personaDTO.Select(e => new TPersonaDTO
        {
            PersonaID = e.NPersonaID,
            UsuarioFK = e.NUsuarioFK,
            TipoIdentificacionfk = e.NTipoIdentificacionFK,
            NroIdentificacion = e.CNroIdentificacion,
            NroContacto = e.CNroConctacto,
            Direccion = e.CDireccion,
            FechaNacimiento = e.DFechaNacimiento,
            Sexo = e.ESexo.ToString()
        });
    }

    public async Task<TPersonaDTO?> GetPersonaIdDTOsAsync(int id)
    {
        var persona = await _tPersonaRepository.GetPersonaIdAsync(id);

        if (persona == null)
        {
            _appLogger.LogError("No se encontró una persona con el ID {id}.", id);
            return null;
        }
        
        _appLogger.LogInformation("Persona con ID {PersonaId} recuperado correctamente.", persona.NPersonaID);

        return new TPersonaDTO
        {
            PersonaID = persona.NPersonaID,
            UsuarioFK = persona.NUsuarioFK,
            TipoIdentificacionfk = persona.NTipoIdentificacionFK,
            NroIdentificacion = persona.CNroIdentificacion,
            NroContacto = persona.CNroConctacto,
            Direccion = persona.CDireccion,
            FechaNacimiento = persona.DFechaNacimiento,
            Sexo = persona.ESexo.ToString()
        };
    }

    public async Task CrearAsync(TPersonaDTO personaDTO)
    {
        var persona = new TPersona
        {
            NUsuarioFK = personaDTO.UsuarioFK,
            NTipoIdentificacionFK = personaDTO.TipoIdentificacionfk,
            CNroIdentificacion = personaDTO.NroIdentificacion,
            CNroConctacto = personaDTO.NroContacto,
            CDireccion = personaDTO.Direccion,
            DFechaNacimiento = personaDTO.FechaNacimiento,
            ESexo = Enum.Parse<ESexo>(personaDTO.Sexo)
        };

        await _tPersonaRepository.AddAsync(persona);
        await _tPersonaRepository.SaveChangeAsync();

        _appLogger.LogInformation("Rol con ID {PersonaId} creado exitosamente.", persona.NPersonaID);
    }

    public async Task ActualizarAsync(int id, TPersonaDTO personaDTO)
    {
        var persona = await _tPersonaRepository.GetPersonaIdAsync(id);

        if (persona == null)
        {
            _appLogger.LogError(null, "Error al actualizar la persona con ID {id}: no existe en el sistema.", id);
        }

        persona.NUsuarioFK = personaDTO.UsuarioFK;
        persona.NTipoIdentificacionFK = personaDTO.TipoIdentificacionfk;
        persona.CNroIdentificacion = personaDTO.NroIdentificacion;
        persona.CNroConctacto = personaDTO.NroContacto;
        persona.CDireccion = personaDTO.Direccion;
        persona.DFechaNacimiento = personaDTO.FechaNacimiento;
        persona.ESexo = Enum.Parse<ESexo>(personaDTO.Sexo);

        _tPersonaRepository.Update(persona);
        await _tPersonaRepository.SaveChangeAsync();

        _appLogger.LogInformation("Persona con ID {PersonaId} actualizado correctamente.", persona.NPersonaID);
    }

    public async Task EliminarAsync(int id)
    {
        var persona = await _tPersonaRepository.GetPersonaIdAsync(id);

        if (persona == null)
        {
            _appLogger.LogError(null, "Error al eliminar la persona con ID {id}: no existe en el sistema.", id);
        }

        _tPersonaRepository.Delete(persona);
        await _tPersonaRepository.SaveChangeAsync();

        _appLogger.LogInformation("Persona con ID {PersonaId} eliminado correctamente.", persona.NPersonaID);
    }
}