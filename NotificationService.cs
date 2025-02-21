using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace son_atik_takip.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly IConfiguration _configuration;

        public NotificationService(IConfiguration configuration, ILogger<NotificationService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public void SendStockAlert(string subject, string message)
        {
            try
            {
                var smtpServer = _configuration["Smtp:Server"];
                var smtpPort = int.Parse(_configuration["Smtp:Port"]);
                var smtpUser = _configuration["Smtp:User"];
                var smtpPass = Environment.GetEnvironmentVariable("SMTP_PASSWORD") ?? _configuration["Smtp:Password"];
                var recipient = _configuration["Smtp:Recipient"];

                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.Credentials = new NetworkCredential(smtpUser, smtpPass);
                    client.EnableSsl = true;
                    var mail = new MailMessage(smtpUser, recipient, subject, message);
                    client.Send(mail);
                }
                _logger.LogInformation("Stok uyarısı e-postası gönderildi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Stok uyarısı e-postası gönderilemedi.");
            }
        }
    }
}
