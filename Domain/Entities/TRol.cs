namespace Api_Mediconnet.Domain.Entities;

public class TRol
{
    public int NRolID { get; set; }
    public string CNombre { get; set; } = null!;
    public virtual ICollection<TUsuarios> Usuarios { get; set; } = null!;
}