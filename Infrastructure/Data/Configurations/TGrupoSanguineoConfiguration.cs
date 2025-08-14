using Api_Mediconnet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Mediconnet.Infrastructure.Data.Configurations;

public class TGrupoSanguineoConfiguration : IEntityTypeConfiguration<TGrupoSanguineo>
{

    public void Configure(EntityTypeBuilder<TGrupoSanguineo> builder)
    {
        builder.ToTable("TGrupoSanguineo");
        builder.HasKey(e => e.NGrupoSanguineoID).HasName("PRIMARY");
        builder.Property(e => e.NGrupoSanguineoID)
            .HasColumnName("NGrupoSanguineoID")
            .HasColumnType("int(12)");

        builder.Property(e => e.CNombre)
            .HasColumnName("CNombre")
            .HasMaxLength(10);
        builder.HasIndex(e => e.CNombre, "CNombre").IsUnique();
    }
}