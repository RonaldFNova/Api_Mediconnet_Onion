using Api_Mediconnet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Mediconnet.Infrastructure.Data.Configurations;

public class TPacienteConfiguration : IEntityTypeConfiguration<TPaciente>
{
    public void Configure(EntityTypeBuilder<TPaciente> builder)
    {
        builder.ToTable("TPaciente");
        builder.HasKey(e => e.NPacienteID).HasName("PRIMARY");
        builder.Property(e => e.NPacienteID)
            .HasColumnName("NPacienteID")
            .HasColumnType("int(32)");

        builder.Property(e => e.NPersonaFK)
            .HasColumnName("NPersonaFK")
            .HasColumnType("int(32)");

        builder.Property(e => e.NGrupoSanguineoFK)
            .HasColumnName("NGrupoSanguineoFK")
            .HasColumnType("int(6)");

        builder.Property(e => e.CAlergiasGenerales)
            .HasColumnName("CAlergiasGenerales")
            .HasMaxLength(2000);

        builder.HasOne(e => e.Personas)
            .WithOne(e => e.Paciente)
            .HasForeignKey<TPersona>(e => e.NPersonaID);
    }
}