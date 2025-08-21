using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;
using Api_Mediconnet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api_Mediconnet.Infrastructure.Repositories;

public class TEspecialidadRepository : ITEspecialidadRepository
{
    private readonly AppDbContext _appDbContext;

    public TEspecialidadRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<TEspecialidad>> GetEspecialidadAsync()
    {
        return await _appDbContext.TEspecialidad.ToListAsync();
    }

    public async Task<TEspecialidad?> GetEspecialidadIdAsync(int id)
    {
        return await _appDbContext.TEspecialidad.FindAsync(id);
    }

    public async Task AddAsync(TEspecialidad especialidad)
    {
        await _appDbContext.TEspecialidad.AddAsync(especialidad);
    }

    public void Update(TEspecialidad especialidad)
    {
        _appDbContext.TEspecialidad.Update(especialidad);
    }

    public void Delete(TEspecialidad especialidad)
    {
        _appDbContext.TEspecialidad.Remove(especialidad);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _appDbContext.SaveChangesAsync() > 0;
    }
}