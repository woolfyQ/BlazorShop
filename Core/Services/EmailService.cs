using Core.Models;
using System.Net.Http.Json;
using Core.InterFaces;

namespace Core.Services
{

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

                Console.WriteLine($"Отправляем запрос к API: {_httpClient.BaseAddress}api/contact");
                Console.WriteLine($"Данные: Name={name}, Email={email}, Message={message}");

                // Отправляем запрос к API
                var response = await _httpClient.PostAsJsonAsync("/api/contact", contactMessage);
                
                Console.WriteLine($"Ответ API: {response.StatusCode}");
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Сообщение успешно отправлено от {name} на API. Ответ: {responseContent}");
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Ошибка API: {response.StatusCode}. Детали: {errorContent}");
                    return false;
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"HTTP ошибка: {httpEx.Message}");
                Console.WriteLine($"Inner exception: {httpEx.InnerException?.Message}");
                return false;
            }
            catch (TaskCanceledException tcEx)
            {
                Console.WriteLine($"Таймаут запроса: {tcEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Общая ошибка отправки сообщения: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return false;
            }
        }
    }
}
