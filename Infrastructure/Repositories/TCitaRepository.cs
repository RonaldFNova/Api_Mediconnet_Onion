using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.Interfaces;
using Api_Mediconnet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api_Mediconnet.Infrastructure.Repositories;

public class TCitaRepository : ITCitaRepository
{
    private readonly AppDbContext _appDbContext;

    public TCitaRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<TCita>> GetCitaAsync()
    {
        return await _appDbContext.TCita.ToListAsync();
    }

    public async Task<TCita?> GetCitaIdAsync(int id)
    {
        return await _appDbContext.TCita.FindAsync(id);
    }

    public async Task AddAsync(TCita cita)
    {
        await _appDbContext.TCita.AddAsync(cita);
    }

    public void Update(TCita cita)
    {
        _appDbContext.TCita.Update(cita);
    }

    public void Delete(TCita cita)
    {
        _appDbContext.TCita.Remove(cita);
    }

    public async Task<bool> SaveChangeAsync()
    {
        return await _appDbContext.SaveChangesAsync() > 0;
    }
}