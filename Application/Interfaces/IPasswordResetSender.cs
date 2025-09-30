namespace Api_Mediconnet.Application.Interfaces;

public interface IPasswordResetSender
{
    Task SendEmailResetPasswordAsync(string emailDestino, string nombreUsuario, string token);
}