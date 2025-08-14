using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;
using Api_Mediconnet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api_Mediconnet.Infrastructure.Repositories;

public class TGrupoSanguineoRepository : ITGrupoSanguineoRepository
{
    private readonly AppDbContext _context;

    public TGrupoSanguineoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TGrupoSanguineo>> GetGrupoSanguineoAsync()
    {
        return await _context.TGrupoSanguineo.ToListAsync();
    }
    public async Task<TGrupoSanguineo?> GetGrupoSanguineoIdAsync(int id)
    {
        return await _context.TGrupoSanguineo.FindAsync(id);
    }
    public async Task AddAsync(TGrupoSanguineo grupoSanguineo)
    {
        await _context.TGrupoSanguineo.AddAsync(grupoSanguineo);
    }
    public void Update(TGrupoSanguineo grupoSanguineo)
    {
        _context.TGrupoSanguineo.Update(grupoSanguineo);
    }
    public void Delete(TGrupoSanguineo grupoSanguineo)
    {
        _context.TGrupoSanguineo.Remove(grupoSanguineo);
    }
    public async Task<bool> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}