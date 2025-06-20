
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Infrastructure.Data.Configurations;

public class TRolConfiguration : IEntityTypeConfiguration<TRol>
{
    public void Configure(EntityTypeBuilder<TRol> builder)
    {
        builder.ToTable("TRol");
        builder.HasKey(e => e.NRolID).HasName("PRIMARY");
        builder.Property(e => e.NRolID)
            .HasColumnName("NRolID")
            .HasColumnType("int(12)");

        builder.Property(e => e.CNombre)
            .HasColumnName("CNombre")
            .HasMaxLength(20);
        builder.HasIndex(e => e.CNombre, "CNombre").IsUnique();
    }
}
