using Microsoft.EntityFrameworkCore;
using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public virtual DbSet<TUsuarios> TUsuarios { get; set; }
    public virtual DbSet<TRol> TRol { get; set; }
    public virtual DbSet<TEstadoVerificacion> TEstadoVerificacion { get; set; }
    public virtual DbSet<TEstadoUsuario> TEstadoUsuario { get; set; }
    public virtual DbSet<TTipoIdentificacion> TTipoIdentificacion { get; set; }
    public virtual DbSet<TLogins> TLogins { get; set; }
    public virtual DbSet<TGrupoSanguineo> TGrupoSanguineo { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TEstadoVerificacion>().HasData(
            new TEstadoVerificacion { NEstadoVerificacionID = 1, CNombre = "Verificado" },
            new TEstadoVerificacion { NEstadoVerificacionID = 2, CNombre = "No Verificado" }
        );

        modelBuilder.Entity<TRol>().HasData(
            new TRol { NRolID = 1, CNombre = "Administrador" },
            new TRol { NRolID = 2, CNombre = "Paciente" },
            new TRol { NRolID = 3, CNombre = "Medico" })
            ;

        modelBuilder.Entity<TEstadoUsuario>().HasData(
            new TEstadoUsuario { NEstadoUsuarioID = 1, CNombre = "Activo" },
            new TEstadoUsuario { NEstadoUsuarioID = 2, CNombre = "Inactivo" }
        );

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
