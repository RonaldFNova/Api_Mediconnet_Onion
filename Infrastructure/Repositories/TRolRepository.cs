using System.Buffers.Binary;
using System.Security.Cryptography;
using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;
using Api_Mediconnet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api_Mediconnet.Infrastructure.Repositories;

public class TRolRepository : ITRolRepository
{
    private readonly AppDbContext _context;

    public TRolRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<TRol>> GetRolAsync()
    {
        return await _context.TRol.ToListAsync();
    }
    public async Task<TRol?> GetRolIdAsync(int id)
    {
        return await _context.TRol.FindAsync(id);
    }
    public async Task AddAsync(TRol rol)
    {
        await _context.TRol.AddAsync(rol);
    }
    public void Update(TRol rol)
    {
        _context.Update(rol);
    }
    public void Delete(TRol rol)
    {
        _context.Remove(rol);
    }
    public async Task<bool> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}