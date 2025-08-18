using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;
using Api_Mediconnet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api_Mediconnet.Infrastructure.Repositories;

public class TPersonaRepository : ITPersonaRepository
{
    private readonly AppDbContext _appDbContext;

    public TPersonaRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<TPersona>> GetPersonaAsync()
    {
        return await _appDbContext.TPersonas.ToListAsync();
    }
    public async Task<TPersona?> GetPersonaIdAsync(int id)
    {
        return await _appDbContext.TPersonas.FindAsync(id);
    }
    public async Task AddAsync(TPersona persona)
    {
        await _appDbContext.TPersonas.AddAsync(persona);
    }
    public void Update(TPersona persona)
    {
        _appDbContext.TPersonas.Update(persona);
    }
    public void Delete(TPersona persona)
    {
        _appDbContext.TPersonas.Remove(persona);
    }

    public async Task<bool> SaveChangeAsync()
    {
        return await _appDbContext.SaveChangesAsync() > 0;
    }
}