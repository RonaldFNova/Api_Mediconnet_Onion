using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.interfaces;

public interface ITAreaService
{
    Task<IEnumerable<TAreaDTO>> GetAreaDTOsAsync();
    Task<TAreaDTO?> GetAreaIdDTOsAsync(int id);
    Task CrearAsync(TAreaDTO areaDto);
    Task<TAreaDTO?> ActualizarAsync(int id, TAreaDTO areaDto);
    Task EliminarAsync(int id);
}