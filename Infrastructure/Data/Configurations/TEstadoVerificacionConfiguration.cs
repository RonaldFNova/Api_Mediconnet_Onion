using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Infrastructure.Data.Configurations;

public class TEstadoVerificacionConfiguration : IEntityTypeConfiguration<TEstadoVerificacion>
{
    public void Configure(EntityTypeBuilder<TEstadoVerificacion> builder)
    {
        builder.ToTable("TEstadoVerificacion");
        builder.HasKey(e => e.NEstadoVerificacionID).HasName("PRIMARY");
        builder.Property(e => e.NEstadoVerificacionID)
            .HasColumnName("NEstadoVerificacion")
            .HasColumnType("int(12)");

        builder.Property(e => e.CNombre)
            .HasColumnName("CNombre")
            .HasMaxLength(30);
        builder.HasIndex(e => e.CNombre, "CNombre").IsUnique();

    }
}