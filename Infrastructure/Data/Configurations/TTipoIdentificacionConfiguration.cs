using Api_Mediconnet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Mediconnet.Infrastructure.Data.Configurations;

public class TTipoIdentificacionConfiguration : IEntityTypeConfiguration<TTipoIdentificacion>
{
    public void Configure(EntityTypeBuilder<TTipoIdentificacion> builder)
    {
        builder.ToTable("TTipoIdentificacion");
        builder.HasKey(e => e.NTipoIdentificacionID).HasName("PRIMARY");
        builder.Property(e => e.NTipoIdentificacionID)
            .HasColumnName("NTipoIdentificacion")
            .HasColumnType("int(12)");

        builder.Property(e => e.CNombre)
            .HasColumnName("CNombre")
            .HasMaxLength(30);
        builder.HasIndex(e => e.CNombre, "CNombre").IsUnique();    }
} 
