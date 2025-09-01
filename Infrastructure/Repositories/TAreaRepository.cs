using Api_Mediconnet.Domain.Entities; 
using Api_Mediconnet.Domain.interfaces;
using Api_Mediconnet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

 namespace Api_Mediconnet.Infrastructure.Repositories;

public class TAreaRepository : ITAreaRepository
{
    private readonly AppDbContext _context;

    public TAreaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TArea>> GetAreaAsync()
    {
        return await _context.TArea.ToListAsync();
    }

    public async Task<TArea?> GetAreaIdAsync(int id)
    {
        return await _context.TArea.FindAsync(id);
    }

    public async Task AddAsync(TArea area)
    {
        await _context.TArea.AddAsync(area);
    }

    public void Update(TArea area)
    {
        _context.TArea.Update(area);
    }

    public void Delete(TArea area)
    {
        _context.TArea.Remove(area);
    }

    public async Task<bool> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}