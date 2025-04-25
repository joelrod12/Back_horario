using System.Net.Mail;
using System.Net;
using Back_horario.Models.Domain.Entities.Email;
using Back_horario.Services.Interface.Email;
using Microsoft.Extensions.Options;

namespace Back_horario.Services.Services.Email
{
    public class EmailServices : IEmailServices
    {
        private readonly EmailSettings _emailSettings;
        public EmailServices(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task<bool> SendResetToken(string toEmail, string token, DateTime? expiration)
        {
            using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password),
                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                Subject = "Restablecimiento de contraseña",
                Body = $@"
                        <!DOCTYPE html>
                        <html>
                        <head>
                          <meta charset='UTF-8'>
                          <style>
                            body {{
                              font-family: Arial, sans-serif;
                              background-color: #f4f4f7;
                              color: #333;
                              padding: 20px;
                            }}
                            .container {{
                              background-color: #ffffff;
                              border-radius: 8px;
                              padding: 30px;
                              max-width: 600px;
                              margin: auto;
                              box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
                            }}
                            .button {{
                              display: inline-block;
                              padding: 12px 20px;
                              margin-top: 20px;
                              background-color: #7F56D9;
                              color: #ffffff;
                              text-decoration: none;
                              border-radius: 5px;
                            }}
                            .footer {{
                              font-size: 12px;
                              margin-top: 30px;
                              color: #777;
                              text-align: center;
                            }}
                          </style>
                        </head>
                        <body>
                          <div class='container'>
                            <h2>Restablecimiento de Contraseña</h2>
                            <p>Hola,</p>
                            <p>Has solicitado restablecer tu contraseña. A continuación encontrarás tu token de recuperación:</p>

                            <p style='font-size: 20px; font-weight: bold; background-color: #eee; padding: 10px; border-radius: 5px;'>
                              {token}
                            </p>

                            <p>Este token es válido hasta:</p>
                            <p><strong>{expiration?.ToLocalTime():dddd, dd MMMM yyyy hh:mm tt}</strong></p>

                            <p>Si no realizaste esta solicitud, puedes ignorar este mensaje.</p>
    
                            <a href='#' class='button'>Restablecer contraseña</a>

                            <div class='footer'>
                              <p>&copy; {DateTime.Now.Year} {_emailSettings.SenderName}. Todos los derechos reservados.</p>
                            </div>
                          </div>
                        </body>
                        </html>",
                IsBodyHtml = true
            };
            mail.To.Add(toEmail);

            await client.SendMailAsync(mail);
            return true;
        }
    }
}

