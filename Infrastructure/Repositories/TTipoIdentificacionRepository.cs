using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.interfaces;
using Api_Mediconnet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api_Mediconnet.Infrastructure.Repositories;


public class TTipoIdentificacionRepository : ITTipoIdentificacionRepository
{
    private readonly AppDbContext _context;

    public TTipoIdentificacionRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<TTipoIdentificacion>> GetTipoIdentificacionAsync()
    {
        return await _context.TTipoIdentificacion.ToListAsync();
    }
    public async Task<TTipoIdentificacion?> GetTipoIdentificacionIdAsync(int id)
    {
        return await _context.TTipoIdentificacion.FindAsync(id);
    }
    public async Task AddAsync(TTipoIdentificacion tipoIdentificacion)
    {
        await _context.AddAsync(tipoIdentificacion);
    }
    public void Update(TTipoIdentificacion tipoIdentificacion)
    {
        _context.Update(tipoIdentificacion);
    }
    public void Delete(TTipoIdentificacion tipoIdentificacion)
    {
        _context.Remove(tipoIdentificacion);
    }
    public async Task<bool> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}