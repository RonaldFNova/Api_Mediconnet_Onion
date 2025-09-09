namespace Api_Mediconnet.Domain.Entities;

public class TRol
{
    public int NRolID { get; set; }
    public string CNombre { get; set; } = null!;
    public virtual ICollection<TUsuario> Usuario { get; set; } = new List<TUsuario>();
}