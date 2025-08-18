namespace Api_Mediconnet.Application.DTOs;


public class TPersonaDTO
{
    public int PersonaID { get; set; }
    public int UsuarioFK { get; set; }
    public int TipoIdentificacionfk { get; set; }
    public string NroIdentificacion { get; set; } = string.Empty;
    public string NroContacto { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public string Sexo { get; set; } = string.Empty;

}
