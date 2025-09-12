namespace Api_Mediconnet.Application.DTOs;

public class TCitaDTO
{
    public int NCitaID { get; set; }
    public int EstadoCitaFK { get; set; }
    public int ProfesionalFK { get; set; }
    public int PacienteFK { get; set; }
    public DateTime Fecha { get; set; }
    public TimeSpan Hora { get; set; }
    public TimeSpan Duracion { get; set; }
    public string Observacion { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; }
}

