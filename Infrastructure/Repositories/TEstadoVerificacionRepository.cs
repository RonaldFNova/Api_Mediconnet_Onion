using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.Interfaces;
using Api_Mediconnet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api_Mediconnet.Infrastructure.Repositories;

public class TEstadoVerificacionRepository : ITEstadoVerificacionRepository
{
    private readonly AppDbContext _context;

    public TEstadoVerificacionRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<TEstadoVerificacion>> GetEstadoVerificacionAsync()
    {
        return await _context.TEstadoVerificacion.ToListAsync();
    }
    public async Task<TEstadoVerificacion?> GetEstadoVerificacionIdAsync(int id)
    {
        return await _context.TEstadoVerificacion.FindAsync(id);
    }
    public async Task AddAsync(TEstadoVerificacion estadoVerificacion)
    {
        await _context.TEstadoVerificacion.AddAsync(estadoVerificacion);
    }
    public void Update(TEstadoVerificacion estadoVerificacion)
    {
        _context.TEstadoVerificacion.Update(estadoVerificacion);
    }
    public void Delete(TEstadoVerificacion estadoVerificacion)
    {
        _context.TEstadoVerificacion.Remove(estadoVerificacion);
    }
    public async Task<bool> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}