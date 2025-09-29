namespace Api_Mediconnet.Application.Interfaces;

public interface IPasswordResetService
{
    Task GenerarTokenResetAsync(string email);
}