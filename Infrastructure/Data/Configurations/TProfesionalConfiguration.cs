using Api_Mediconnet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Mediconnet.Infrastructure.Data.Configurations;

public class TProfesionalConfiguration : IEntityTypeConfiguration<TProfesional>
{
    public void Configure(EntityTypeBuilder<TProfesional> builder)
    {
        builder.ToTable("TProfesional");
        builder.HasKey(e => e.NProfesionalID).HasName("PRIMARY");
        builder.Property(e => e.NProfesionalID)
            .HasColumnName("NProfesionalID")
            .HasColumnType("int(32)");

        builder.Property(e => e.NPersonaFK)
            .HasColumnName("NPersonaFK")
            .HasColumnType("int(32)");

        builder.Property(e => e.CRegistroProfesional)
            .HasColumnName("CRegistroProfesional")
            .HasMaxLength(50);
        builder.HasIndex(e => e.CRegistroProfesional, "CRegistroProfesional").IsUnique();

        builder.Property(e => e.DFechaContratacion)
            .HasColumnName("DFechaContratacion")
            .HasColumnType("DateTime");

        builder.Property(e => e.ETipoProfesional)
            .HasColumnName("ETipoProfesional")
            .HasConversion<string>()
            .HasMaxLength(25);

        builder.Property(e => e.CBiografia)
            .HasColumnName("CBiografia")
            .HasMaxLength(2000);

        builder.HasOne(e => e.Personas)
            .WithOne(p => p.Profesional)
            .HasForeignKey<TProfesional>(e => e.NPersonaFK)
            .IsRequired(false);

        builder.HasOne(e => e.Especialidad)
            .WithMany(u => u.Profesional)
            .HasForeignKey(u => u.NEspecialidadFK)
            .HasConstraintName("FK_Profesional_Especialidad")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Area)
            .WithMany(U => U.Profesional)
            .HasForeignKey(u => u.NAreaFK)
            .HasConstraintName("FK_Profesional_Area")
            .OnDelete(DeleteBehavior.Restrict);

    }
}