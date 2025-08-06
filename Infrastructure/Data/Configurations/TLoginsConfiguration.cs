using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Api_Mediconnet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api_Mediconnet.Infrastructure.Data.Configurations;

public class TLoginsConfiguration : IEntityTypeConfiguration<TLogins>
{

    public void Configure(EntityTypeBuilder<TLogins> builder)
    {
        builder.ToTable("TLogins");
        builder.HasKey(e => e.NLoginID).HasName("PRIMARY");
        builder.Property(e => e.NLoginID)
            .HasColumnName("NLoginID")
            .HasColumnType("int(32)");

        builder.Property(e => e.DFechaLogin)
            .HasColumnName("DFechaLogin")
            .HasColumnType("DateTime");
    }
}