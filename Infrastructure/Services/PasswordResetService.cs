using System.Net;
using System.Net.Mail;
using Api_Mediconnet.Application.Interfaces;

namespace Api_Mediconnet.Infrastructure.Services;

public class PasswordSendResetService : IPasswordSendResetService
{
    private readonly string _smtpUser;
    private readonly string _smtpPass;

    public PasswordSendResetService()
    {
        _smtpUser = Environment.GetEnvironmentVariable("GMAIL_USER")
                  ?? throw new Exception("GMAIL_USER not configured");
         _smtpPass = Environment.GetEnvironmentVariable("GMAIL_PASS")
                  ?? throw new Exception("GMAIL_PASS not configured");
    }

    public async Task SendEmailResetPasswordAsync(string emailDestino, string nombreUsuario, string token)
    {
        var subject = "Restablece tu contraseña";

        var resetLink = $"https://midominio.com/reset-password?token={token}";

        var htmlContent = $@"
        <!DOCTYPE html>
        <html lang='es'>
        <head>
        <meta charset='UTF-8'>
        <style>
            body {{
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
            margin: 0;
            padding: 0;
            }}
            .container {{
            background-color: #ffffff;
            max-width: 600px;
            margin: 30px auto;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            }}
            .header {{
            text-align: center;
            padding-bottom: 20px;
            }}
            .brand {{
            font-size: 32px;
            font-weight: bold;
            color: #26a541;
            margin-bottom: 10px;
            }}
            .title {{
            font-size: 24px;
            margin-bottom: 10px;
            color: #333333;
            }}
            .message {{
            font-size: 16px;
            color: #555555;
            margin-bottom: 20px;
            line-height: 1.6;
            }}
            .button {{
            display: inline-block;
            padding: 12px 24px;
            background-color: #26a541;
            color: #ffffff !important;
            font-size: 18px;
            font-weight: bold;
            text-decoration: none;
            border-radius: 6px;
            margin: 20px 0;
            }}
            .info {{
            font-size: 14px;
            color: #777;
            margin-top: 30px;
            border-top: 1px solid #e0e0e0;
            padding-top: 15px;
            }}
            .footer {{
            text-align: center;
            font-size: 12px;
            color: #999999;
            margin-top: 20px;
            }}
            a {{
            color: #1a73e8;
            text-decoration: none;
            }}
        </style>
        </head>
        <body>
        <div class='container'>
            <div class='header'>
            <div class='brand'>Mediconnet</div>
            <div class='title'>Restablecer tu contraseña</div>
            </div>

            <div class='message'>
            Hola {nombreUsuario},<br><br>
            Hemos recibido una solicitud para restablecer la contraseña de tu cuenta.
            Haz clic en el siguiente botón para continuar:
            </div>

            <div style='text-align: center;'>
            <a href='{resetLink}' class='button'>Restablecer Contraseña</a>
            </div>

            <div class='message'>
            Si no solicitaste este cambio, puedes ignorar este correo de forma segura.
            </div>

            <div class='info'>
            <strong>Información de seguridad:</strong><br>
            - El enlace expirará en 15 minutos.<br>
            - Nunca compartas este enlace con nadie.
            </div>

            <div class='footer'>
            © 2025 MediConnet. Todos los derechos reservados.<br>
            <a href='#'>Ayuda</a> • <a href='#'>Política de Privacidad</a> • <a href='#'>Términos del servicio</a>
            </div>
        </div>
        </body>
        </html>";

        using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
        {
            smtpClient.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
            smtpClient.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpUser, "MediConnet"),
                Subject = subject,
                Body = htmlContent,
                IsBodyHtml = true
            };

            mailMessage.To.Add(emailDestino);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}