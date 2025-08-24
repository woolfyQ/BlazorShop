using System.Net.Mail;
using System.Net;

namespace MyRoof.API.Services
{
    public interface IEmailService
    {
        Task<bool> SendContactFormAsync(string name, string email, string message);
    }

    public class EmailService : IEmailService
    {
        public async Task<bool> SendContactFormAsync(string name, string email, string message)
        {
            try
            {
                var smtpServer = "smtp-mail.outlook.com";
                var smtpPort = 587;
                var username = "TeamYourRoof@outlook.com";
                var password = "ykjvtiadxyeotlzd";
                var fromEmail = "TeamYourRoof@outlook.com";
                var toEmail = "TeamYourRoof@outlook.com";

                using var client = new SmtpClient(smtpServer, smtpPort)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(username, password)
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail, "MyRoof Contact Form"),
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

                mailMessage.To.Add(toEmail);

                await client.SendMailAsync(mailMessage);
                Console.WriteLine($"Email успешно отправлен от {name} на {toEmail}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка отправки email: {ex.Message}");
                Console.WriteLine($"Детали ошибки: {ex.ToString()}");
                return false;
            }
        }
    }
}
