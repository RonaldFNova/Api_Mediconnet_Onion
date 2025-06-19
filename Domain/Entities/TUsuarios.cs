namespace Api_Mediconnet.Domain.Entities;

public class TUsuarios
{
    public int NUsuarioID { get; set; }
    public string CNombre { get; set; } = null!;
    public string CApellido { get; set; } = null!;
    public string CEmail { get; set; } = null!;
    public string CPassword { get; set; } = null!;
}