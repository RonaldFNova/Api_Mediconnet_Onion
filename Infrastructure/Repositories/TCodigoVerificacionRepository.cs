using Api_Mediconnet.Domain.Entities;
using Api_Mediconnet.Domain.Interfaces;
using Api_Mediconnet.Domain.Enums;
using Api_Mediconnet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api_Mediconnet.Infrastructure.Repositories;

public class TCodigoVerificacionRepository : ITCodigoVerificacionRepository
{
    private readonly AppDbContext _context;

    public TCodigoVerificacionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TCodigoVerificacion>> GetCodigoVerificacionAsync()
    {
        return await _context.TCodigoVerificacion.ToListAsync();
    }

    public async Task<TCodigoVerificacion?> GetCodigoVerificacionIdAsync(int id)
    {
        return await _context.TCodigoVerificacion.FindAsync(id);
    }

    public async Task AddAsync(TCodigoVerificacion codigoVerificacion)
    {
        await _context.TCodigoVerificacion.AddAsync(codigoVerificacion);
    }

    public void Update(TCodigoVerificacion codigoVerificacion)
    {
        _context.TCodigoVerificacion.Update(codigoVerificacion);
    }

    public void Delete(TCodigoVerificacion codigoVerificacion)
    {
        _context.TCodigoVerificacion.Remove(codigoVerificacion);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<TCodigoVerificacion?> GetCodigoUserFkAsync(int usuarioId)
    {
        return await _context.TCodigoVerificacion
            .Where(c => c.NUsuarioFK == usuarioId
                    && c.ETipoCodigo == TipoCodigoVerificacion.Email
                    && c.BUsado == false)
            .OrderByDescending(c => c.DFechaCreacion)
            .FirstOrDefaultAsync();
    }

    public async Task<TCodigoVerificacion?> GetCodigoUserEmailAsync(string email)
    {
        return await _context.TCodigoVerificacion
            .Where(c => c.Usuario.CEmail == email
                    && c.ETipoCodigo == TipoCodigoVerificacion.Email
                    && c.BUsado == false)
            .OrderByDescending(c => c.DFechaCreacion)
            .FirstOrDefaultAsync();
    }

}