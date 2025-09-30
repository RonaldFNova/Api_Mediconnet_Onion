using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.Interfaces;

public interface IEmailVerificacionService
{
    Task GenerarCodigoVerificacionAsync(string email);
    Task<ValidarCodigoVerificacionResponseDTO> ValidarCodigoVerificacionAsync(string email, string codigo);
}