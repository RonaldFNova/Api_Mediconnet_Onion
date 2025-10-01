using Api_Mediconnet.Application.DTOs;

namespace Api_Mediconnet.Application.Interfaces;

public interface IPasswordResetService
{
    Task GenerarTokenResetAsync(string email);
    Task ResetPasswordAsync(PasswordResetDTO passwordResetDTO);
}