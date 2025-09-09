using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.Interfaces;
using Api_Mediconnet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api_Mediconnet.Infrastructure.Repositories;

public class TPacienteRepository : ITPacienteRepository
{
    private readonly AppDbContext _appDbContext;

    public TPacienteRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<TPaciente>> GetPacienteAsync()
    {
        return await _appDbContext.TPaciente.ToListAsync();
    }

    public async Task<TPaciente?> GetPacienteIdAsync(int id)
    {
        return await _appDbContext.TPaciente.FindAsync(id);
    }

    public async Task AddAsync(TPaciente paciente)
    {
        await _appDbContext.TPaciente.AddAsync(paciente);
    }

    public void Update(TPaciente paciente)
    {
        _appDbContext.TPaciente.Update(paciente);
    }

    public void Delete(TPaciente paciente)
    {
        _appDbContext.TPaciente.Remove(paciente);
    }

    public async Task<bool> SaveChangeAsync()
    {
        return await _appDbContext.SaveChangesAsync() > 0;
    }
}