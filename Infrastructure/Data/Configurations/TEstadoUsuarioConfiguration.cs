using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Infrastructure.Data.Configurations;

public class TEstadoUsuarioConfiguration : IEntityTypeConfiguration<TEstadoUsuario>
{
    public void Configure(EntityTypeBuilder<TEstadoUsuario> builder)
    {
        builder.ToTable("TEstadoUsuario");
        builder.HasKey(e => e.NEstadoUsuarioID).HasName("PRIMARY");
        builder.Property(e => e.NEstadoUsuarioID)
            .HasColumnName("NEstadoUsuarioID")
            .HasColumnType("int(12)");

        builder.Property(e => e.CNombre)
            .HasColumnName("CNombre")
            .HasMaxLength(20);
        builder.HasIndex(e => e.CNombre, "CNombre").IsUnique();
    }
}