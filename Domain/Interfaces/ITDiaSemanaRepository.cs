using Api_Mediconnet.Domain.Entities;

namespace Api_Mediconnet.Domain.interfaces;

public interface ITDiaSemanaRepository
{
    Task<IEnumerable<TDiaSemana>> GetDiaSemanaAsync();
    Task<TDiaSemana?> GetDiaSemanaIdAsync(int id);
    Task AddAsync(TDiaSemana diaSemana);
    void Update(TDiaSemana diaSemana);
    void Delete(TDiaSemana diaSemana);
    Task<bool> SaveChangeAsync();
}