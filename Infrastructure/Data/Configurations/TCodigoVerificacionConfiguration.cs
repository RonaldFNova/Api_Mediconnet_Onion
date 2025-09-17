using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Api_Mediconnet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api_Mediconnet.Infrastructure.Data.Configurations;

public class TCodigoVerificacionConfiguration : IEntityTypeConfiguration<TCodigoVerificacion>
{
    public void Configure(EntityTypeBuilder<TCodigoVerificacion> builder)
    {
        builder.ToTable("TCodigoVerificacion");
        builder.HasKey(e => e.NCodigoVerificacionID).HasName("PRIMARY");
        builder.Property(e => e.NCodigoVerificacionID)
            .HasColumnName("NCodigoVerificacionID")
            .HasColumnType("int(32)");

        builder.Property(e => e.NUsuarioFK)
            .HasColumnName("NUsuarioFK")
            .HasColumnType("int(6)");

        builder.Property(e => e.CCodigo)
            .HasColumnName("CCodigo")
            .HasMaxLength(255);
        builder.HasIndex(e => e.CCodigo, "CCodigo").IsUnique();

        builder.Property(e => e.DFechaExpiracion)
            .HasColumnName("DFechaExpiracion")
            .HasColumnType("datetime");

        builder.Property(e => e.BUsado)
            .HasColumnName("BUsado")
            .HasColumnType("tinyint(1)")
            .HasDefaultValue(false);

        builder.Property(e => e.DFechaCreacion)
            .HasColumnName("DFechaCreacion")
            .HasColumnType("datetime");

        builder.Property(e => e.NIntentos)
            .HasColumnName("NIntentos")
            .HasColumnType("int(11)");

        builder.Property(e => e.NTipoCodigo)
            .HasColumnName("NTipoCodigo")
            .HasConversion<string>()
            .HasMaxLength(25);

        builder.HasOne(e => e.Usuario)
            .WithMany(u => u.CodigoVerificacion)
            .HasForeignKey(u => u.NUsuarioFK)
            .HasConstraintName("FK_CodigoVerificacion_Usuario")
            .OnDelete(DeleteBehavior.Restrict);
    }
}           

