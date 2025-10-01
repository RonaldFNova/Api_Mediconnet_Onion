using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.Interfaces;

public interface IEmailVerificacionService
{
    Task<StatusCodeDTO> GenerarCodigoVerificacionAsync(string email);
    Task<StatusCodeDTO> ValidarCodigoVerificacionAsync(string email, string codigo);
}