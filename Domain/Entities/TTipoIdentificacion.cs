namespace Api_Mediconnet.Domain.Entities;

public class TTipoIdentificacion
{
    public int NTipoIdentificacionID { get; set; }
    public string CNombre { get; set; } = null!;
    public virtual ICollection<TPersona> Personas { get; set; } = null!;
}