using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.interfaces;

public interface ITLoginsService
{
    Task<IEnumerable<TloginsDTO>> GetLoginsDTOsAsync();

    Task<TloginsDTO?> GetLoginsIdDTOsAsync(int id);

    Task<string> CrearAsync(LoginsRequestDTO LoginsRequest);

    Task ActualizarAsync(int id, TloginsDTO tloginsDTO);

    Task EliminarAsync(int id);
}