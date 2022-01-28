using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace E_Shopper_UI.EmailServices
{
    public class EmailSender : IEmailSender
    {
        private const string SendGridKey = "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";
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(SendGridKey, subject, htmlMessage, email);
        }

        private Task Execute(string sendGridKey, string subject, string message, string email)
        {
            var client = new SendGridClient(sendGridKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress("eedemirkazik@gmail.com", " E-Shopper"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };

            msg.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(msg);
        }


    }
}
