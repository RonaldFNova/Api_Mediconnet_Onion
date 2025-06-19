using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Infrastructure.Data.Configurations;

public class TregistroConfiguration : IEntityTypeConfiguration<Tregistro>
{
    public void Configure(EntityTypeBuilder<Tregistro> builder)
    {
        builder.ToTable("Tregistro");
        builder.HasKey(e => e.NUserId).HasName("PRIMARY");

        builder.Property(e => e.NUserId).HasColumnName("NUserId");
    }
}

