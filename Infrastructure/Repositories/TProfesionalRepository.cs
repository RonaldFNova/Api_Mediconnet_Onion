using Api_Mediconnet.Domain.interfaces;
using Api_Mediconnet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Api_Mediconnet.Infrastructure.Data;

namespace Api_Mediconnet.Infrastructure.Repositories;

public class TProfesionalRepository : ITProfesionalRepository
{
    private readonly AppDbContext _context;
    public TProfesionalRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TProfesional>> GetProfesionalesAsync()
    {
        return await _context.TProfesional.ToListAsync();
    }
    public async Task<TProfesional?> GetProfesionalIdAsync(int id)
    {
        return await _context.TProfesional.FindAsync(id);
    }
    public async Task AddAsync(TProfesional profesional)
    {
        await _context.TProfesional.AddAsync(profesional);
    }
    public void Update(TProfesional profesional)
    {
        _context.TProfesional.Update(profesional);
    }
    public void Delete(TProfesional profesional)
    {
        _context.TProfesional.Remove(profesional);
    }
    public async Task<bool> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}