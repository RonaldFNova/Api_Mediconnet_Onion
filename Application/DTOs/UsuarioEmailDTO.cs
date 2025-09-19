namespace Api_Mediconnet.Application.DTOs;

public class UsuarioEmailDTO
{
    public string NombreCompleto { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int UsuarioID { get; set; }
}