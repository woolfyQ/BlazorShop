namespace Core.InterFaces
{
    public interface IEmailService
    {
        Task<bool> SendContactFormAsync(string name, string email, string message);
        
    }
}