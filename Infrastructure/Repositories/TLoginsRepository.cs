using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;
using Api_Mediconnet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api_Mediconnet.Infrastructure.Repositories;

public class TLoginsRepository : ITLoginsRepository
{
    private readonly AppDbContext _context;
    public TLoginsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TLogins>> GetLoginsAsync()
    {
        return await _context.TLogins.ToListAsync();
    }

    public async Task<TLogins?> GetLoginsIdAsync(int id)
    {
        return await _context.TLogins.FindAsync(id);
    }

    public async Task<TUsuarios?> GetByEmailAsync(string email)
    {
         return await _context.TUsuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.CEmail == email);
    }

    public async Task AddAsync(TLogins logins)
    {
        await _context.TLogins.AddAsync(logins);
    }

    public void Update(TLogins logins)
    {
        _context.TLogins.Update(logins);
    }

    public void Delete(TLogins logins)
    {
        _context.TLogins.Remove(logins);
    }

    public async Task<bool> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

}