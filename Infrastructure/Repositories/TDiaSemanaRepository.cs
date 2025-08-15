using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;
using Api_Mediconnet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api_Mediconnet.Infrastructure.Repositories;

public class TDiaSemanaRepository : ITDiaSemanaRepository
{
    private readonly AppDbContext _appDbContext;

    public TDiaSemanaRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<TDiaSemana>> GetDiaSemanaAsync()
    {
        return await _appDbContext.TDiaSemana.ToListAsync();
    }

    public async Task<TDiaSemana?> GetDiaSemanaIdAsync(int id)
    {
        return await _appDbContext.TDiaSemana.FindAsync(id);
    }

    public async Task AddAsync(TDiaSemana diaSemana)
    {
        await _appDbContext.TDiaSemana.AddAsync(diaSemana);
    }

    public void Update(TDiaSemana diaSemana)
    {
        _appDbContext.TDiaSemana.Update(diaSemana);
    }

    public void Delete(TDiaSemana diaSemana)
    {
        _appDbContext.TDiaSemana.Remove(diaSemana);
    }

    public async Task<bool> SaveChangeAsync()
    {
        return await _appDbContext.SaveChangesAsync() > 0;
    }

}