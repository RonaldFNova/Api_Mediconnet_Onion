using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;



namespace Api_Mediconnet.Application.Services;

public class TUsuariosService : ITUsuariosService
{
    private readonly IHashPasswordService _servicioHashPassword;
    private readonly ITUsuariosRepository _tUsuariosRepository;
     private readonly IJwtTokenIdService _jwtTokenIdService;
    public TUsuariosService(ITUsuariosRepository tUsuariosRepository, IHashPasswordService servicioHashPassword, IJwtTokenIdService jwtTokenIdService)
    {
        _tUsuariosRepository = tUsuariosRepository;
        _servicioHashPassword = servicioHashPassword;
        _jwtTokenIdService = jwtTokenIdService;
    }

    public async Task<IEnumerable<TUsuarioResponseDTO>> GetUsuariosAsync()
    {
        var Usuarios = await _tUsuariosRepository.GetUsuariosAsync();
        return Usuarios.Select(u => new TUsuarioResponseDTO
        {
            NUsuarioID = u.NUsuarioID,
            CNombre = u.CNombre,
            CApellido = u.CApellido,
            CEmail = u.CEmail,
            NRolFK = u.NRolFK,
            NEstadoUsuarioFK = u.NEstadoUsuarioFK,
            NEstadoVerificacionFK = u.NEstadoVerificacionFK
        });
    }

    public async Task<TUsuarioResponseDTO?> GetUsuariosIdAsync(int id)
    {
        var usuario = await _tUsuariosRepository.GetUsuariosIdAsync(id);
        if (usuario == null) return null;

        return new TUsuarioResponseDTO
        {
            NUsuarioID = usuario.NUsuarioID,
            CNombre = usuario.CNombre,
            CApellido = usuario.CApellido,
            CEmail = usuario.CEmail,
            NRolFK = usuario.NRolFK,
            NEstadoUsuarioFK = usuario.NEstadoUsuarioFK,
            NEstadoVerificacionFK = usuario.NEstadoVerificacionFK
        };
    }

    public async Task<string> CrearAsync(TUsuarioCreateDTO dTO)
    {
        var usuario = new TUsuarios
        {
            CNombre = dTO.CNombre,
            CApellido = dTO.CApellido,
            CEmail = dTO.CEmail,
            CPassword = _servicioHashPassword.Hash(dTO.CPassword),
            NRolFK = dTO.NRolFK,
            NEstadoUsuarioFK = 1,
            NEstadoVerificacionFK = 2,
            DFechaRegistro = DateTime.UtcNow
        };
        await _tUsuariosRepository.AddAsync(usuario);
        await _tUsuariosRepository.SaveChangesAsync();

        var rol = await  _tUsuariosRepository.GetRolNombreByUsuarioIdAsync(usuario.NUsuarioID);
        string token = _jwtTokenIdService.GenerarToken(Convert.ToString(usuario.NUsuarioID), Convert.ToString(rol)!);

        return token;
    }

    public async Task ActualizarAsync(int id, TUsuarioCreateDTO dTO)
    {
        var usuario = await _tUsuariosRepository.GetUsuariosIdAsync(id);

        if (usuario == null) return;

        usuario.CNombre = dTO.CNombre;
        usuario.CApellido = dTO.CApellido;
        usuario.CEmail = dTO.CEmail;
        usuario.CPassword = dTO.CPassword;
        usuario.NRolFK = dTO.NRolFK;
        usuario.NEstadoUsuarioFK = dTO.NEstadoUsuarioFK;
        usuario.NEstadoVerificacionFK = dTO.NEstadoVerificacionFK;

        _tUsuariosRepository.Update(usuario);
        await _tUsuariosRepository.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var usuario = await _tUsuariosRepository.GetUsuariosIdAsync(id);

        if (usuario == null) return;

        _tUsuariosRepository.Delete(usuario);
        await _tUsuariosRepository.SaveChangesAsync();
    }
}