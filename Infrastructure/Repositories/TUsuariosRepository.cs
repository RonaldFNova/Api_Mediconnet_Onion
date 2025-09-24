
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.Interfaces;
using Api_Mediconnet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api_Mediconnet.Infrastructure.Repositories;

public class TUsuarioRepository : ITUsuarioRepository
{
    private readonly AppDbContext _context;

    public TUsuarioRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<TUsuario>> GetUsuarioAsync()
    {
        return await _context.TUsuario.ToListAsync();
    }
    public async Task<TUsuario?> GetUsuarioIdAsync(int id)
    {
        return await _context.TUsuario.FindAsync(id);
    }
    public async Task AddAsync(TUsuario Usuario)
    {
        await _context.TUsuario.AddAsync(Usuario);
    }
    public async Task<string?> GetRolNombreByUsuarioIdAsync(int id)
    {
        return await _context.TUsuario.Where(u => u.NUsuarioID == id)
            .Select(u => u.Rol.CNombre)
            .FirstOrDefaultAsync();
    }
    public void Update(TUsuario Usuario)
    {
        _context.TUsuario.Update(Usuario);
    }
    public void Delete(TUsuario Usuario)
    {
        _context.TUsuario.Remove(Usuario);
    }
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<TUsuario?> GetUsuarioEmailAsync(string email)
    {
        return await _context.TUsuario.Where(u => u.CEmail == email)
            .FirstOrDefaultAsync();
    }

}