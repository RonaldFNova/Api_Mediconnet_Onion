using Api_Mediconnet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Mediconnet.Infrastructure.Data.Configurations;

public class TCitaConfiguration : IEntityTypeConfiguration<TCita>
{
    public void Configure(EntityTypeBuilder<TCita> builder)
    {
        builder.ToTable("TCita");
        builder.HasKey(e => e.NCitaID).HasName("PRIMARY");
        builder.Property(e => e.NCitaID)
            .HasColumnName("NCitaID")
            .HasColumnType("int(12)");

        builder.Property(e => e.NProfesionalFK)
            .HasColumnName("NProfesionalFK")
            .HasColumnType("int(12)");

        builder.Property(e => e.NEstadoCitaFK)
            .HasColumnName("NEstadoCitaFK")
            .HasColumnType("int(12)");

        builder.Property(e => e.NPacienteFK)
            .HasColumnName("NPacienteFK")
            .HasColumnType("int(12)");

        builder.Property(e => e.DFecha)
            .HasColumnName("DFecha")
            .HasColumnType("datetime");

        builder.Property(e => e.DHora)
            .HasColumnName("DHora")
            .HasColumnType("time");

        builder.Property(e => e.DDuracion)
            .HasColumnName("DDuracion")
            .HasColumnType("time");

        builder.Property(e => e.CObservacion)
            .HasColumnName("CObservacion")
            .HasMaxLength(500);

        builder.Property(e => e.DFechaRegistro)
            .HasColumnName("DFechaRegistro")
            .HasColumnType("datetime");


        builder.HasOne(e => e.DiaSemana)
            .WithMany(u => u.Cita)
            .HasForeignKey(u => u.NDiaSemanaFK)
            .HasConstraintName("FK_Cita_DiaSemana")
            .OnDelete(DeleteBehavior.Restrict);

   }         
}