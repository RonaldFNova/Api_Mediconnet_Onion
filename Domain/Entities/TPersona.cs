using Api_Mediconnet.Domain.Enums;

namespace Api_Mediconnet.Domain.Entities;

public class TPersona
{
    public int NPersonaID { get; set; }
    public int NUsuarioFK { get; set; }
    public int NTipoIdentificacionFK { get; set; }
    public string CNroIdentificacion { get; set; } = null!;
    public string CNroConctacto { get; set; } = null!;
    public string CDireccion { get; set; } = null!;
    public DateTime DFechaNacimiento { get; set; }
    public Sexo ESexo { get; set; }
    public TUsuario Usuario { get; set; } = null!;
    public TPaciente? Paciente { get; set; }
    public TProfesional? Profesional { get; set; }
    public virtual TTipoIdentificacion TipoIdentificacion { get; set; } = null!;

}