using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace SistemaGestaoVigilanciaGP2018.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
           
        }

        public AuthMessageSenderOptions Options { get; } // Set via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY"), subject, message, email);
        }//System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY")--no azure

        public Task Execute(string apiKey, string subject, string message, string email)
        {
             //apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("SistemaVigilancia@projecto.com", "Grupo 2GP"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message,
            };
            msg.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(msg);
        }
    }
}
