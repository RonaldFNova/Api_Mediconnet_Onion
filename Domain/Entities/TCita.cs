namespace Api_Mediconnet.Domain.Entities;

public class TCita
{
    public int NCitaID { get; set; }
    public int NEstadoCitaFK { get; set; }
    public int NProfesionalFK { get; set; }
    public int NPacienteFK { get; set; }
    public DateTime DFecha { get; set; }
    public TimeSpan DHora { get; set; }
    public TimeSpan DDuracion { get; set; }
    public string CObservacion { get; set; } = null!;
    public DateTime DFechaRegistro { get; set; }
    public virtual TProfesional Profesional { get; set; } = null!;
    public virtual TPaciente Paciente { get; set; } = null!;
    public virtual TEstadoCita Estado { get; set; } = null!;
}