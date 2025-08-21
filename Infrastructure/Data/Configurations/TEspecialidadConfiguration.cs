using Api_Mediconnet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Mediconnet.Infrastructure.Data.Configurations;

public class TEspecialidadConfiguration : IEntityTypeConfiguration<TEspecialidad>
{
    public void Configure(EntityTypeBuilder<TEspecialidad> builder)
    {
        builder.ToTable("TEspecialidad");
        builder.HasKey(e => e.NEspecialidadID).HasName("PRIMARY");
        builder.Property(e => e.NEspecialidadID)
            .HasColumnName("NEspecialidadID")
            .HasColumnType("int(12)");

        builder.Property(e => e.CNombre)
            .HasColumnName("CNombre")
            .HasMaxLength(30);
        builder.HasIndex(e => e.CNombre, "CNombre").IsUnique();

        builder.Property(e => e.CDescripcion)
            .HasColumnName("CDescripcion")
            .HasMaxLength(100);
    }
}