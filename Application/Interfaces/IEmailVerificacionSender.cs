namespace Api_Mediconnet.Application.Interfaces;

public interface IEmailVerificacionSender
{
    Task SendVerificationCodeAsync(string emailDestino, string nombreUsuario, string codigoVerificacion);
}