using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.Interfaces;

public interface IEmailVerificacionService
{
    Task GenerarCodeVerificationAsync(string email);
}