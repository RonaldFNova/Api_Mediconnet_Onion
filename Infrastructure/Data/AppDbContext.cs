using Microsoft.EntityFrameworkCore;
using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public virtual DbSet<TUsuario> TUsuario { get; set; }
    public virtual DbSet<TRol> TRol { get; set; }
    public virtual DbSet<TEstadoVerificacion> TEstadoVerificacion { get; set; }
    public virtual DbSet<TEstadoUsuario> TEstadoUsuario { get; set; }
    public virtual DbSet<TTipoIdentificacion> TTipoIdentificacion { get; set; }
    public virtual DbSet<TLogins> TLogins { get; set; }
    public virtual DbSet<TGrupoSanguineo> TGrupoSanguineo { get; set; }
    public virtual DbSet<TDiaSemana> TDiaSemana { get; set; }
    public virtual DbSet<TPersona> TPersona { get; set; }
    public virtual DbSet<TPaciente> TPaciente { get; set; }
    public virtual DbSet<TEspecialidad> TEspecialidad { get; set; }
    public virtual DbSet<TProfesional> TProfesional { get; set; }
    public virtual DbSet<TArea> TArea { get; set; }    
    public virtual DbSet<TEstadoCita> TEstadoCita { get; set; }
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

        modelBuilder.Entity<TGrupoSanguineo>().HasData(
            new TGrupoSanguineo { NGrupoSanguineoID = 1, CNombre = "A+" },
            new TGrupoSanguineo { NGrupoSanguineoID = 2, CNombre = "A-" },
            new TGrupoSanguineo { NGrupoSanguineoID = 3, CNombre = "B+" },
            new TGrupoSanguineo { NGrupoSanguineoID = 4, CNombre = "B-" },
            new TGrupoSanguineo { NGrupoSanguineoID = 5, CNombre = "AB+" },
            new TGrupoSanguineo { NGrupoSanguineoID = 6, CNombre = "AB-" },
            new TGrupoSanguineo { NGrupoSanguineoID = 7, CNombre = "O+" },
            new TGrupoSanguineo { NGrupoSanguineoID = 8, CNombre = "O-" }
        );

        modelBuilder.Entity<TTipoIdentificacion>().HasData(
            new TTipoIdentificacion { NTipoIdentificacionID = 1, CNombre = "Cédula de ciudadanía" },
            new TTipoIdentificacion { NTipoIdentificacionID = 2, CNombre = "Tarjeta de identidad" },
            new TTipoIdentificacion { NTipoIdentificacionID = 3, CNombre = "Registro civil de nacimiento" },
            new TTipoIdentificacion { NTipoIdentificacionID = 4, CNombre = "Cédula de extranjería" },
            new TTipoIdentificacion { NTipoIdentificacionID = 5, CNombre = "Pasaporte" }
        );

        modelBuilder.Entity<TDiaSemana>().HasData(
            new TDiaSemana { NDiaSemanaID = 1, CNombre = "Lunes" },
            new TDiaSemana { NDiaSemanaID = 2, CNombre = "Martes" },
            new TDiaSemana { NDiaSemanaID = 3, CNombre = "Miércoles" },
            new TDiaSemana { NDiaSemanaID = 4, CNombre = "Jueves" },
            new TDiaSemana { NDiaSemanaID = 5, CNombre = "Viernes" },
            new TDiaSemana { NDiaSemanaID = 6, CNombre = "Sábado" },
            new TDiaSemana { NDiaSemanaID = 7, CNombre = "Domingo" }
        );

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
