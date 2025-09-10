namespace MyRoof.API.Models
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; } = string.Empty;
        public int SmtpPort { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;
        public string ToEmail { get; set; } = string.Empty;
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public string DeliveryMethod { get; set; } = "Network";
        public int Timeout { get; set; } = 30000;
    }
}
