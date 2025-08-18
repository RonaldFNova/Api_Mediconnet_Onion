namespace Api_Mediconnet.Domain.Entities;

public class TUsuarios
{
    public int NUsuarioID { get; set; }
    public int NEstadoVerificacionFK { get; set; }
    public virtual TEstadoVerificacion EstadoVerificacion { get; set; } = null!;
    public int NEstadoUsuarioFK { get; set; }
    public virtual TEstadoUsuario EstadoUsuario { get; set; } = null!;
    public int NRolFK { get; set; }
    public virtual TRol Rol { get; set; } = null!;
    public string CNombre { get; set; } = null!;
    public string CApellido { get; set; } = null!;
    public string CEmail { get; set; } = null!;
    public string CPassword { get; set; } = null!;
    public DateTime DFechaRegistro { get; set; }
    public virtual ICollection<TLogins> Logins { get; set; } = null!;
    public TPersona Personas { get; set; } = null!;

}