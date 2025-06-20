using Microsoft.EntityFrameworkCore;
using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {

    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public virtual DbSet<TUsuarios> TUsuarios { get; set; }
    public virtual DbSet<TRol> TRol { get; set; }
    public virtual DbSet<TEstadoVerificacion> TEstadoVerificacion { get; set; }
    public virtual DbSet<TEstadoUsuario> TEstadoUsuario { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
