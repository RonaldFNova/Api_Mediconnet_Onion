using Api_Mediconnet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Mediconnet.Infrastructure.Data.Configurations;

public class TDiaSemanaConfiguration : IEntityTypeConfiguration<TDiaSemana>
{

    public void Configure(EntityTypeBuilder<TDiaSemana> builder)
    {
        builder.ToTable("TDiaSemana");
        builder.HasKey(e => e.NDiaSemanaID).HasName("PRIMARY");
        builder.Property(e => e.NDiaSemanaID)
            .HasColumnName("NDiaSemanaID")
            .HasColumnType("int(12)");

        builder.Property(e => e.CNombre)
            .HasColumnName("CNombre")
            .HasMaxLength(30);
        builder.HasIndex(e => e.CNombre, "CNombre").IsUnique();      
    }
}