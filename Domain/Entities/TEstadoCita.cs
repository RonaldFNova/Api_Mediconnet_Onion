namespace Api_Mediconnet.Domain.Entities;

public class TEstadoCita
{
    public int NEstadoCitaID { get; set; }
    public string CNombre { get; set; } = null!;    
    public virtual ICollection<TCita> Cita { get; set; } = new List<TCita>();
}