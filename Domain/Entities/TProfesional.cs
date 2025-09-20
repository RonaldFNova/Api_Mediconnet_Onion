using Api_Mediconnet.Domain.Enums;

namespace Api_Mediconnet.Domain.Entities;

public class TProfesional
{
    public int NProfesionalID { get; set; }
    public int NPersonaFK { get; set; }
    public int NEspecialidadFK { get; set; }
    public int NAreaFK { get; set; }
    public string CRegistroProfesional { get; set; } = null!;
    public DateTime DFechaContratacion { get; set; }
    public Profesional ETipoProfesional { get; set; }
    public string CBiografia { get; set; } = null!;
    public TPersona Personas { get; set; } = null!;
    public TEspecialidad Especialidad { get; set; } = null!;
    public TArea Area { get; set; } = null!;
}