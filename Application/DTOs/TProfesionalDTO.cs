namespace Api_Mediconnet.Application.DTOs;

public class TProfesionalDTO
{
    public int ProfesionalID { get; set; }
    public int PersonaFK { get; set; }
    public string RegistroProfesional { get; set; } = string.Empty;
    public DateTime FechaContratacion { get; set; }
    public string TipoProfesional { get; set; } = string.Empty;
    public string Biografia { get; set; } = string.Empty;
}