using Api_Mediconnet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Mediconnet.Infrastructure.Data.Configurations;

public class TEstadoCitaConfiguration : IEntityTypeConfiguration<TEstadoCita>
{
    public void Configure(EntityTypeBuilder<TEstadoCita> builder)
    {
        builder.ToTable("TEstadoCita");
        builder.HasKey(e => e.NEstadoCitaID).HasName("PRIMARY");
        builder.Property(e => e.NEstadoCitaID)
            .HasColumnName("NEstadoCitaID")
            .HasColumnType("int(6)");

        builder.Property(e => e.CNombre)
            .HasColumnName("CNombre")
            .HasMaxLength(30);
        builder.HasIndex(e => e.CNombre, "CNombre").IsUnique();
    }
}
