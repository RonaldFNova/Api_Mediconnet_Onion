namespace Api_Mediconnet.Application.DTOs;

public class TCodigoVerificacionDTO
{
    public int CodigoVerificacionID { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public DateTime FechaExpiracion { get; set; }
    public bool Usado { get; set; }
    public int UsuarioFK { get; set; }
    public string TipoCodigo { get; set; } = string.Empty;
    public int Intentos { get; set; }
    public DateTime FechaCreacion { get; set; }
}