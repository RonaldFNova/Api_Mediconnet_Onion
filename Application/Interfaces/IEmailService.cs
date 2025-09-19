namespace Api_Mediconnet.Application.Interfaces;

public interface IEmailService
{
    Task SendEmailCodeAsync(string emailDestino, string nombreUsuario, int usuarioId);
}