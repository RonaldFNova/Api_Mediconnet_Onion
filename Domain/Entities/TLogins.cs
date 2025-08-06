
namespace Api_Mediconnet.Domain.Entities;

public class TLogins
{
    public int NLoginID { get; set; }
    public DateTime DFechaLogin { get; set; }
    public virtual TUsuarios Usuarios { get; set; } = null!;
    public int NUsuarioFK { get; set; }
}
