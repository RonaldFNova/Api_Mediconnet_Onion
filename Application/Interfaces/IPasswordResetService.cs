using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.Interfaces;

public interface IPasswordResetService
{
    Task<StatusCodeDTO> GenerarTokenResetAsync(string email);
    Task<StatusCodeDTO> ResetPasswordAsync(PasswordResetDTO passwordResetDTO);
}