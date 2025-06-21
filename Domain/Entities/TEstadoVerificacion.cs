namespace Api_Mediconnet.Domain.Entities;

public class TEstadoVerificacion
{
    public int NEstadoVerificacionID { get; set; }
    public string CNombre { get; set; } = null!;
    public virtual ICollection<TUsuarios> Usuarios { get; set; } = null!;
    
}
