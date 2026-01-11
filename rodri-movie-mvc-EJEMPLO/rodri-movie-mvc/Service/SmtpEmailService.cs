using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace rodri_movie_mvc.Service
{
    // Usamos una interface de .Net para definir el contrato del servicio de email
    // Al usar una interface, podemos cambiar la implementación del servicio sin afectar al resto de la aplicación
    // Por ejemplo, podríamos implementar un servicio que use una API de terceros en lugar de SMTP
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string htmlBody, string? textBody = null);
    }

    public sealed class SmtpEmailService : IEmailService
    {
        private readonly SmtpSettings _cfg;
        public SmtpEmailService(IOptions<SmtpSettings> cfg) => _cfg = cfg.Value;

        public async Task SendAsync(string to, string subject, string htmlBody, string? textBody = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_cfg.FromName, _cfg.User));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            var builder = new BodyBuilder { HtmlBody = htmlBody, TextBody = textBody ?? string.Empty };
            message.Body = builder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_cfg.Host, _cfg.Port, _cfg.UseStartTls ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto);
            await client.AuthenticateAsync(_cfg.User, _cfg.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }

    public sealed class SmtpSettings
    {
        public string Host { get; set; } = "";
        public int Port { get; set; }
        public string User { get; set; } = "";
        public string Password { get; set; } = "";
        public string FromName { get; set; } = "Sistema";
        public bool UseStartTls { get; set; } = true;
    }
}