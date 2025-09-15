namespace Api_Mediconnet.Domain.Entities;

public class TDiaSemana
{
    public int NDiaSemanaID { get; set; }
    public string CNombre { get; set; } = null!;
    public ICollection<TCita> Cita { get; set; } = new List<TCita>();
}