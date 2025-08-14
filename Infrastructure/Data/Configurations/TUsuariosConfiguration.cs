using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Infrastructure.Data.Configurations;

public class TUsuariosConfiguration : IEntityTypeConfiguration<TUsuarios>
{
    public void Configure(EntityTypeBuilder<TUsuarios> builder)
    {
        builder.ToTable("TUsuarios");
        builder.HasKey(e => e.NUsuarioID).HasName("PRIMARY");
        builder.Property(e => e.NUsuarioID)
            .HasColumnName("nUsuarioID")
            .HasColumnType("int(32)");

        builder.Property(e => e.NEstadoVerificacionFK)
            .HasColumnName("NEstadoVerificacionFK")
            .HasColumnType("int(12)");

        builder.Property(e => e.NRolFK)
            .HasColumnName("NRolFK")
            .HasColumnType("int(12)");

        builder.Property(e => e.NEstadoUsuarioFK)
            .HasColumnName("NEstadoUsuarioFK")
            .HasColumnType("int(12)");

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

        builder.Property(e => e.DFechaRegistro)
            .HasColumnName("DFechaRegistro")
            .HasColumnType("DateTime");

        builder.HasOne(u => u.EstadoVerificacion)
            .WithMany(e => e.Usuarios)
            .HasForeignKey(u => u.NEstadoVerificacionFK)
            .HasConstraintName("FK_Usuarios_EstadoVerificacion")
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(u => u.EstadoUsuario)
            .WithMany(e => e.Usuarios)
            .HasForeignKey(u => u.NEstadoUsuarioFK)
            .HasConstraintName("FK_Usuarios_EstadoUsuarios")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.Rol)
            .WithMany(e => e.Usuarios)
            .HasForeignKey(u => u.NRolFK)
            .HasConstraintName("FK_Usuarios_Rol")
            .OnDelete(DeleteBehavior.Restrict);
    }
}

