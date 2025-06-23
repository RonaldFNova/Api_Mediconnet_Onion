using Api_Mediconnet.Domain.interfaces;
using Api_Mediconnet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Api_Mediconnet.Infrastructure.Data;

namespace Api_Mediconnet.Infrastructure.Repositories;
public class TEstadoUsuarioRepository : ITEstadoUsuarioRepository
{
    private readonly AppDbContext _context;
    public TEstadoUsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TEstadoUsuario>> GetEstadoUsuarioAsync()
    {
        return await _context.TEstadoUsuario.ToListAsync();
    }
    public async Task<TEstadoUsuario?> GetEstadoUsuarioIdAsync(int id)
    {
        return await _context.TEstadoUsuario.FindAsync(id);
    }
    public async Task AddAsync(TEstadoUsuario estadoUsuario)
    {
        await _context.TEstadoUsuario.AddAsync(estadoUsuario);
    }
    public void Update(TEstadoUsuario estadoUsuario)
    {
        _context.TEstadoUsuario.Update(estadoUsuario);
    }
    public void Delete(TEstadoUsuario estadoUsuario)
    {
        _context.TEstadoUsuario.Remove(estadoUsuario);
    }
    public async Task<bool> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}

