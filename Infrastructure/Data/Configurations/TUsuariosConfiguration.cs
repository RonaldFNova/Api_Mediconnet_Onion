using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Infrastructure.Data.Configurations;

public class TUsuariosConfiguration : IEntityTypeConfiguration<TUsuarios>
{
    public void Configure(EntityTypeBuilder<TUsuarios> builder)
    {
        builder.ToTable("tUsuarios");
        builder.HasKey(e => e.NUsuarioID).HasName("PRIMARY");
        builder.Property(e => e.NUsuarioID)
            .HasColumnName("nUsuarioID")
            .HasColumnType("int(32)");

        builder.Property(e => e.CEmail)
            .HasColumnName("CEmail")
            .HasMaxLength(255);
        builder.HasIndex(e => e.CEmail, "CEmail").IsUnique();

        builder.Property(e => e.CNombre)
            .HasColumnName("CNombre")
            .HasMaxLength(50);

        builder.Property(e => e.CApellido)
            .HasColumnName("CApellido")
            .HasMaxLength(100);

        builder.Property(e => e.CPassword)
            .HasColumnName("CPassword")
            .HasMaxLength(255);

    }
}

