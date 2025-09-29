namespace Api_Mediconnet.Application.Interfaces;

public interface IPasswordSendResetService
{
    Task SendEmailResetPasswordAsync(string emailDestino, string nombreUsuario, string token);
}