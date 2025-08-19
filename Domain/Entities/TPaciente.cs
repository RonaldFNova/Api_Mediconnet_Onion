namespace Api_Mediconnet.Domain.Entities;

public class TPaciente
{
    public int NPacienteID { get; set; }
    public int NPersonaFK { get; set; }
    public int NGrupoSanguineoFK { get; set; }
    public string CAlergiasGenerales { get; set; } = null!;
    public TPersona Personas { get; set; } = null!;

}