using Api_Mediconnet.Application.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Api_Mediconnet.Infrastructure.Services;

public class EmailVerificacionSender : IEmailVerificacionSender
{
  private readonly string _smtpUser;
  private readonly string _smtpPass;

  public EmailVerificacionSender()
  {
    _smtpUser = Environment.GetEnvironmentVariable("GMAIL_USER")
                  ?? throw new Exception("GMAIL_USER not configured");
    _smtpPass = Environment.GetEnvironmentVariable("GMAIL_PASS")
                  ?? throw new Exception("GMAIL_PASS not configured");
  }

  public async Task SendVerificationCodeAsync(string emailDestino, string nombreUsuario, string codigoVerificacion)
  {


    var subject = "Verifica tu dirección de correo electrónico";

    var plainTextContent = $"Hola {nombreUsuario}, tu código de verificación es: {codigoVerificacion}";

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
                }}
                .code-box {{
                  background-color: #f0f0f0;
                  padding: 20px;
                  border-radius: 5px;
                  font-size: 28px;
                  letter-spacing: 2px;
                  color: #111;
                  font-weight: bold;
                  text-align: center;
                  margin-bottom: 20px;
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
                  <div class='title'>Verifica tu dirección de correo electrónico</div>
                </div>
                <div class='message'>
                  Hola {nombreUsuario},<br><br>
                  Para completar la configuración de tu cuenta, por favor ingresa el siguiente código de verificación:
                </div>
                <div class='code-box'>
                  {codigoVerificacion}
                </div>
                <div class='message'>
                  Introduce este código en la página de verificación.
                </div>

                <div class='info'>
                  <strong>Información de seguridad:</strong><br>
                  - Este código expirará en 24 horas.<br>
                  - Si no solicitaste este código, puedes ignorar este correo de forma segura.
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

