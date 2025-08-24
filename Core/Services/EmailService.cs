using System.Text.Json;
using Core.Models;
using System.Net.Http.Json;

namespace Core.Services
{
    public interface IEmailService
    {
        Task<bool> SendContactFormAsync(string name, string email, string message);
    }

    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;

        public EmailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SendContactFormAsync(string name, string email, string message)
        {
            try
            {
                // Создаем объект сообщения
                var contactMessage = new ContactMessage
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    Message = message,
                    Timestamp = DateTime.Now,
                    Status = "Отправлено"
                };

                // Отправляем запрос к API
                var response = await _httpClient.PostAsJsonAsync("/api/contact", contactMessage);
                
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Сообщение успешно отправлено от {name} на API");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Ошибка API: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка отправки сообщения: {ex.Message}");
                return false;
            }
        }
    }
}
