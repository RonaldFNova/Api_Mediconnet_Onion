using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;

namespace Api_Mediconnet.Application.Services;

public class TPersonaService : ITPersonaService
{
    private readonly ITPersonaRepository _tPersonaRepository;

    public TPersonaService(ITPersonaRepository tPersonaRepository)
    {
        _tPersonaRepository = tPersonaRepository;
    }

    public async Task<IEnumerable<TPersonaDTO>> GetPersonaDTOsAsync()
    {
        var personaDTO = await _tPersonaRepository.GetPersonaAsync();

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

        if (persona == null) return null;

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
    }

    public async Task ActualizarAsync(int id, TPersonaDTO personaDTO)
    {
        var persona = await _tPersonaRepository.GetPersonaIdAsync(id);

        if (persona == null) return;

        persona.NUsuarioFK = personaDTO.UsuarioFK;
        persona.NTipoIdentificacionFK = personaDTO.TipoIdentificacionfk;
        persona.CNroIdentificacion = personaDTO.NroIdentificacion;
        persona.CNroConctacto = personaDTO.NroContacto;
        persona.CDireccion = personaDTO.Direccion;
        persona.DFechaNacimiento = personaDTO.FechaNacimiento;
        persona.ESexo = Enum.Parse<ESexo>(personaDTO.Sexo);

        _tPersonaRepository.Update(persona);
        await _tPersonaRepository.SaveChangeAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var persona = await _tPersonaRepository.GetPersonaIdAsync(id);

        if (persona == null) return;

        _tPersonaRepository.Delete(persona);
        await _tPersonaRepository.SaveChangeAsync();
    }
}