namespace Api_Mediconnet.Application.DTOs;

public class TPacienteDTO
{
    public int PacienteID { get; set; }
    public int PersonaFK { get; set; }
    public int GrupoSanguineoFK { get; set; }
    public string AlergiasGenerales { get; set; } = string.Empty;
}
