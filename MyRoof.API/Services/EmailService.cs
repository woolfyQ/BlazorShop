using System.Net.Mail;
using System.Net;
using MyRoof.API.Models;
using Microsoft.Extensions.Options;

namespace MyRoof.API.Services
{
    public interface IEmailService
    {
        Task<bool> SendContactFormAsync(string name, string email, string message);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task<bool> SendContactFormAsync(string name, string email, string message)
        {
            // Пробуем разные настройки SMTP
            var smtpConfigs = new[]
            {
                new { Port = 587, EnableSsl = true, UseDefaultCredentials = false },
                new { Port = 465, EnableSsl = true, UseDefaultCredentials = false },
                new { Port = 25, EnableSsl = false, UseDefaultCredentials = false }
            };

            foreach (var config in smtpConfigs)
            {
                try
                {
                    using var client = new SmtpClient(_emailSettings.SmtpServer, config.Port)
                    {
                        EnableSsl = config.EnableSsl,
                        UseDefaultCredentials = config.UseDefaultCredentials,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Timeout = _emailSettings.Timeout
                    };

                    // Явно устанавливаем аутентификацию
                    client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_emailSettings.FromEmail, "MyRoof Contact Form"),
                        Subject = $"Новое сообщение от {name}",
                        Body = $@"
                            <h3>Новое сообщение с сайта MyRoof</h3>
                            <p><strong>Имя:</strong> {name}</p>
                            <p><strong>Email:</strong> {email}</p>
                            <p><strong>Сообщение:</strong></p>
                            <p>{message}</p>
                            <hr>
                            <p><em>Сообщение отправлено с сайта MyRoof</em></p>",
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(_emailSettings.ToEmail);

                    await client.SendMailAsync(mailMessage);
                    Console.WriteLine($"Email успешно отправлен от {name} на {_emailSettings.ToEmail} (порт: {config.Port}, SSL: {config.EnableSsl})");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка отправки email (порт: {config.Port}, SSL: {config.EnableSsl}): {ex.Message}");
                    // Продолжаем пробовать следующую конфигурацию
                }
            }

            Console.WriteLine("Все конфигурации SMTP не сработали");
            return false;
        }
    }
}
