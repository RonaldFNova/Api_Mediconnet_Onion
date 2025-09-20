using Api_Mediconnet.Domain.Enums;

namespace Api_Mediconnet.Domain.Entities;

public class TCodigoVerificacion
{
    public int NCodigoVerificacionID { get; set; }
    public int NUsuarioFK { get; set; }
    public string CCodigo { get; set; } = null!;
    public TipoCodigoVerificacion ETipoCodigo { get; set; }
    public DateTime DFechaExpiracion { get; set; }
    public bool BUsado { get; set; }
    public int NIntentos { get; set; }
    public DateTime DFechaCreacion { get; set; }
    public virtual TUsuario Usuario { get; set; } = null!;
}