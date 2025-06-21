
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;
using Api_Mediconnet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api_Mediconnet.Infrastructure.Repositories;

public class TUsuarioRepository : ITUsuariosRepository
{
    private readonly AppDbContext _context;

    public TUsuarioRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<TUsuarios>> GetUsuariosAsync()
    {
        return await _context.TUsuarios.ToListAsync();
    }
    public async Task<TUsuarios?> GetUsuariosIdAsync(int id)
    {
        return await _context.TUsuarios.FindAsync(id);
    }
    public async Task AddAsync(TUsuarios usuarios)
    {
        await _context.TUsuarios.AddAsync(usuarios);
    }
    public void Update(TUsuarios usuarios)
    {
        _context.TUsuarios.Update(usuarios);
    }
    public void Delete(TUsuarios usuarios)
    {
        _context.TUsuarios.Remove(usuarios);
    }
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

}