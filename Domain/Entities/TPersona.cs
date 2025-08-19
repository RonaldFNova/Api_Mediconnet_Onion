namespace Api_Mediconnet.Domain.Entities;

public enum ESexo
{
    Masculino = 1,
    Femenino = 2
}

public class TPersona
{
    public int NPersonaID { get; set; }
    public int NUsuarioFK { get; set; }
    public int NTipoIdentificacionFK { get; set; }
    public string CNroIdentificacion { get; set; } = null!;
    public string CNroConctacto { get; set; } = null!;
    public string CDireccion { get; set; } = null!;
    public DateTime DFechaNacimiento { get; set; }
    public ESexo ESexo { get; set; }
    public TUsuarios Usuarios { get; set; } = null!;
    public TPaciente Paciente { get; set; } = null!;
    public virtual TTipoIdentificacion TipoIdentificacion { get; set; } = null!;

}