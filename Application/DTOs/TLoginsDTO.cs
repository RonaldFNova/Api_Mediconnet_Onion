namespace Api_Mediconnet.Application.DTOs;

public class TloginsDTO
{
    public int LoginID { get; set; }
    public DateTime FechaLogin { get; set; } 
    public int UsuarioFK { get; set; }
}