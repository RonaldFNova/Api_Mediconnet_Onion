namespace Api_Mediconnet.Domain.Entities;

public class TEspecialidad
{
    public int NEspecialidadID { get; set; }
    public string CNombre { get; set; } = null!;
    public string CDescripcion { get; set; } = null!;
    public virtual ICollection<TProfesional> Profesional { get; set; } = new List<TProfesional>();

}