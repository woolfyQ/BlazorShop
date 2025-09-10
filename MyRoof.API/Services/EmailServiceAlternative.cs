using MyRoof.API.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace MyRoof.API.Services
{
    /// <summary>
    /// Альтернативная версия EmailService с улучшенной обработкой SMTP
    /// </summary>
    public class EmailServiceAlternative : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailServiceAlternative(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task<bool> SendContactFormAsync(string name, string email, string message)
        {
            try
            {
                // Создаем SMTP клиент с правильными настройками
                using var client = new SmtpClient();
                
                // Настройки сервера
                client.Host = _emailSettings.SmtpServer;
                client.Port = _emailSettings.SmtpPort;
                client.EnableSsl = _emailSettings.EnableSsl;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Timeout = _emailSettings.Timeout;

                // Аутентификация
                client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);

                // Создаем сообщение
                using var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_emailSettings.FromEmail, "MyRoof Contact Form");
                mailMessage.To.Add(_emailSettings.ToEmail);
                mailMessage.Subject = $"Новое сообщение от {name}";
                mailMessage.Body = $@"
                    <h3>Новое сообщение с сайта MyRoof</h3>
                    <p><strong>Имя:</strong> {name}</p>
                    <p><strong>Email:</strong> {email}</p>
                    <p><strong>Сообщение:</strong></p>
                    <p>{message}</p>
                    <hr>
                    <p><em>Сообщение отправлено с сайта MyRoof</em></p>";
                mailMessage.IsBodyHtml = true;

                // Отправляем сообщение
                await client.SendMailAsync(mailMessage);
                
                Console.WriteLine($"Email успешно отправлен от {name} на {_emailSettings.ToEmail}");
                return true;
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"SMTP ошибка: {smtpEx.Message}");
                Console.WriteLine($"Статус код: {smtpEx.StatusCode}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Общая ошибка отправки email: {ex.Message}");
                Console.WriteLine($"Детали ошибки: {ex.ToString()}");
                return false;
            }
        }
    }
}
