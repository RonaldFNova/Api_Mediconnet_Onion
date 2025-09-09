namespace Api_Mediconnet.Domain.Entities;

public class TGrupoSanguineo
{
    public int NGrupoSanguineoID { get; set; }
    public string CNombre { get; set; } = null!;
    public ICollection<TPaciente> Paciente { get; set; } = new List<TPaciente>();

}