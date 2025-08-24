using Microsoft.AspNetCore.Mvc;
using Core.Models;
using MyRoof.API.Services;

namespace MyRoof.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public ContactController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] ContactMessage message)
        {
            try
            {
                var success = await _emailService.SendContactFormAsync(
                    message.Name, 
                    message.Email, 
                    message.Message
                );

                if (success)
                {
                    return Ok(new { message = "Сообщение успешно отправлено" });
                }
                else
                {
                    return BadRequest(new { message = "Ошибка при отправке сообщения" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Внутренняя ошибка сервера", error = ex.Message });
            }
        }
    }
}
