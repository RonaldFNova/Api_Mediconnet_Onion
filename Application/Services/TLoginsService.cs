using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;

namespace Api_Mediconnet.Application.Services;

public class TLoginsService : ITLoginsService
{
    private readonly ITLoginsRepository _tLoginsRepository;
    private readonly IHashPasswordService _hashPasswordService;
    private readonly IJwtTokenIdService _jwtTokenIdService;
    public TLoginsService(ITLoginsRepository tLoginsRepository, IHashPasswordService hashPasswordService, IJwtTokenIdService jwtTokenIdService)
    {
        _tLoginsRepository = tLoginsRepository;
        _hashPasswordService = hashPasswordService;
        _jwtTokenIdService = jwtTokenIdService;
    }

    public async Task<IEnumerable<TloginsDTO>> GetLoginsDTOsAsync()
    {
        var Logins = await _tLoginsRepository.GetLoginsAsync();
        return Logins.Select(e => new TloginsDTO
        {
            NLoginID = e.NLoginID,
            DFechaLogin = e.DFechaLogin
        });
    }

    public async Task<TloginsDTO?> GetLoginsIdDTOsAsync(int id)
    {
        var logins = await _tLoginsRepository.GetLoginsIdAsync(id);

        if (logins == null) return null;

        return new TloginsDTO
        {
            NLoginID = logins.NLoginID,
            DFechaLogin = logins.DFechaLogin
        };
    }

    public async Task<string> CrearAsync(LoginsRequestDTO loginsRequest)
    {

        var user = await _tLoginsRepository.GetByEmailAsync(loginsRequest.CEmail);
        if (user == null) throw new Exception("Usuario no encontrado");

        var result = _hashPasswordService.Verificar(loginsRequest.CPassword, user.CPassword);
        if (!result) throw new Exception("La contrseña es incorrecta");

        var NewLogins = new TLogins
        {
            DFechaLogin = DateTime.UtcNow,
            NUsuarioFK = user.NUsuarioID
        };
        
        var token = _jwtTokenIdService.GenerarToken(user.NUsuarioID.ToString(), user.Rol.CNombre);

        await _tLoginsRepository.AddAsync(NewLogins);
        await _tLoginsRepository.SaveChangeAsync();

        return token;        
    }

    public async Task ActualizarAsync(int id, TloginsDTO DTOs)
    {
        var logins = await _tLoginsRepository.GetLoginsIdAsync(id);

        if (logins == null) return;

        logins.DFechaLogin = DTOs.DFechaLogin;

        _tLoginsRepository.Update(logins);
        await _tLoginsRepository.SaveChangeAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var logins = await _tLoginsRepository.GetLoginsIdAsync(id);

        if (logins == null) return;

        _tLoginsRepository.Delete(logins);
        await _tLoginsRepository.SaveChangeAsync();
    }
}