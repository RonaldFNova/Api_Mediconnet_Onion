using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Api_Mediconnet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api_Mediconnet.Infrastructure.Data.Configurations;

public class TAreaConfiguration : IEntityTypeConfiguration<TArea>
{
    public void Configure(EntityTypeBuilder<TArea> builder)
    {
        builder.ToTable("TArea");
        builder.HasKey(e => e.NAreaID).HasName("PRIMARY");
        builder.Property(e => e.NAreaID)
            .HasColumnName("NAreaID")
            .HasColumnType("int(12)");

        builder.Property(e => e.CNombre)
            .HasColumnName("CNombre")
            .HasMaxLength(30);
        builder.HasIndex(e => e.CNombre, "CNombre").IsUnique();

        builder.Property(e => e.CDescripcion)
            .HasColumnName("CDescripcion")
            .HasMaxLength(1000);
    }
}