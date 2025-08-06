using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Api_Mediconnet.Application.DTOs;
using Api_Mediconnet.Application.interfaces;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;

namespace Api_Mediconnet.Application.Services;

public class TLoginsService : ITLoginsService
{
    private readonly ITLoginsRepository _tLoginsRepository;

    public TLoginsService(ITLoginsRepository tLoginsRepository)
    {
        _tLoginsRepository = tLoginsRepository;
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

    public async Task CrearAsync(TloginsDTO DTOs)
    {
        var logins = new TLogins
        {
            NLoginID = DTOs.NLoginID,
            DFechaLogin = DateTime.UtcNow
        };

        await _tLoginsRepository.AddAsync(logins);
        await _tLoginsRepository.SaveChangeAsync();
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