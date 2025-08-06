namespace Api_Mediconnet.Application.DTOs;

public class TUsuarioCreateDTO
{
    public int UsuarioID { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Apellido { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public int RolFK { get; set; }

    public int EstadoUsuarioFK { get; set; }
    
    public int EstadoVerificacionFK { get; set; }
}