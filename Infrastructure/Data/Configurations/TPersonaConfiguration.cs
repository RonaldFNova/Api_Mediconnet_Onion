using Api_Mediconnet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Mediconnet.Infrastructure.Data.Configurations;

public class TPersonaConfiguration : IEntityTypeConfiguration<TPersona>
{
    public void Configure(EntityTypeBuilder<TPersona> builder)
    {
        builder.ToTable("TPersona");
        builder.HasKey(e => e.NPersonaID).HasName("PRIMARY");
        builder.Property(e => e.NPersonaID)
            .HasColumnName("NPersonaID")
            .HasColumnType("int(32)");

        builder.Property(e => e.NUsuarioFK)
            .HasColumnName("NUsuarioFK")
            .HasColumnType("int(32)");

        builder.Property(e => e.NTipoIdentificacionFK)
            .HasColumnName("NTipoIdentificacionFK")
            .HasColumnType("int(32)");

        builder.Property(e => e.CDireccion)
            .HasColumnName("CDireccion")
            .HasMaxLength(255);

        builder.Property(e => e.CNroConctacto)
            .HasColumnName("CNroConctacto")
            .HasMaxLength(30);

        builder.Property(e => e.DFechaNacimiento)
            .HasColumnName("DFechaNacimiento")
            .HasColumnType("DateTime");

        builder.Property(e => e.ESexo)
            .HasColumnName("ESexo")
            .HasConversion<string>()
            .HasMaxLength(25);

        builder.Property(e => e.CNroIdentificacion)
            .HasColumnName("CNroIdentificacion")
            .HasMaxLength(20);
        builder.HasIndex(e => e.CNroIdentificacion, "CNroIdentificacion").IsUnique();

        builder.HasOne(u => u.TipoIdentificacion)
            .WithMany(e => e.Personas)
            .HasForeignKey(u => u.NTipoIdentificacionFK)
            .HasConstraintName("FK_Personas_TipoIdentificacion")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Usuario)
            .WithOne(p => p.Persona)
            .HasForeignKey<TPersona>(e => e.NUsuarioFK)
            .IsRequired(false);
    }
}