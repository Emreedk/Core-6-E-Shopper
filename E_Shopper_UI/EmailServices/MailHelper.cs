using System.Net;
using System.Net.Mail;

namespace E_Shopper_UI.EmailServices
{
    public class MailHelper
    {
        public static bool SendMail(string body, string to, string subject, bool isHtml = true)
        {
            return SendMail(body, new List<string> { to }, subject, isHtml);
        }

        public static bool SendMail(string body, List<string> to, string subject, bool isHtml = true)
        {
            bool result = false;

            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("eedemirkazik@gmail.com");

                to.ForEach(x =>
                {
                    message.To.Add(new MailAddress(x));
                });

                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;

                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.EnableSsl = true;
                    smtp.Credentials =
                        new NetworkCredential("eedemirkazik@gmail.com","emreedk");
                    smtp.Send(message);
                    result = true;
                }

            }
            catch (Exception)
            {
                //LOG
            }
            return result;
        }
    }
}
