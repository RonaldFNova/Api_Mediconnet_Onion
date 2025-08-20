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
            UsuarioID = u.NUsuarioID,
            Nombre = u.CNombre,
            Apellido = u.CApellido,
            Email = u.CEmail,
            RolFK = u.NRolFK,
            EstadoUsuarioFK = u.NEstadoUsuarioFK,
            EstadoVerificacionFK = u.NEstadoVerificacionFK
        });
    }

    public async Task<TUsuarioResponseDTO?> GetUsuariosIdAsync(int id)
    {
        var usuario = await _tUsuariosRepository.GetUsuariosIdAsync(id);
        if (usuario == null) return null;

        return new TUsuarioResponseDTO
        {
            UsuarioID = usuario.NUsuarioID,
            Nombre = usuario.CNombre,
            Apellido = usuario.CApellido,
            Email = usuario.CEmail,
            RolFK = usuario.NRolFK,
            EstadoUsuarioFK = usuario.NEstadoUsuarioFK,
            EstadoVerificacionFK = usuario.NEstadoVerificacionFK
        };
    }

    public async Task<string> CrearAsync(TUsuarioCreateDTO dTO)
    {
        var usuario = new TUsuarios
        {
            CNombre = dTO.Nombre,
            CApellido = dTO.Apellido,
            CEmail = dTO.Email,
            CPassword = _servicioHashPassword.Hash(dTO.Password),
            NRolFK = dTO.RolFK,
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

        usuario.CNombre = dTO.Nombre;
        usuario.CApellido = dTO.Apellido;
        usuario.CEmail = dTO.Email;
        usuario.CPassword = dTO.Password;
        usuario.NRolFK = dTO.RolFK;
        usuario.NEstadoUsuarioFK = dTO.EstadoUsuarioFK;
        usuario.NEstadoVerificacionFK = dTO.EstadoVerificacionFK;

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