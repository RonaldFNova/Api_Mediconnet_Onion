using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.Interfaces;
using Api_Mediconnet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api_Mediconnet.Infrastructure.Repositories;

public class TEstadoCitaRepository : ITEstadoCitaRepository
{
    private readonly AppDbContext _appDbContext;

    public TEstadoCitaRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<TEstadoCita>> GetEstadoCitaAsync()
    {
        return await _appDbContext.TEstadoCita.ToListAsync();
    }

    public async Task<TEstadoCita?> GetEstadoCitaIdAsync(int id)
    {
        return await _appDbContext.TEstadoCita.FindAsync(id);
    }

    public async Task AddAsync(TEstadoCita estadoCita)
    {
        await _appDbContext.TEstadoCita.AddAsync(estadoCita);
    }

    public void Update(TEstadoCita estadoCita)
    {
        _appDbContext.TEstadoCita.Update(estadoCita);
    }

    public void Delete(TEstadoCita estadoCita)
    {
        _appDbContext.TEstadoCita.Remove(estadoCita);
    }

    public async Task<bool> SaveChangeAsync()
    {
        return await _appDbContext.SaveChangesAsync() > 0;
    }

}