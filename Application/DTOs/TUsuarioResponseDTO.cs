
namespace Api_Mediconnet.Application.DTOs;

public class TUsuarioResponseDTO
{
    public int NUsuarioID { get; set; }
    public string CNombre { get; set; } = string.Empty;
    public string CApellido { get; set; } = string.Empty;
    public string CEmail { get; set; } = string.Empty;
    public int NRolFK { get; set; }
    public int NEstadoUsuarioFK { get; set; }
    public int NEstadoVerificacionFK { get; set; }
}