using ajansflix.CORE.EmailConfig;
using ajansflix.SERVICES.Interface;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ajansflix.SERVICES.Service
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        public EmailSender(IOptions<EmailConfiguration> emailConfig)
        {
            _emailConfig = emailConfig.Value;
        }


        public async Task<string> SendEmailAsync(CustomerBook book)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(MailboxAddress.Parse(_emailConfig.From));
                emailMessage.To.Add(MailboxAddress.Parse(book.To));
                emailMessage.Subject = book.Subject;
                emailMessage.Date = DateTime.Now;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("<div>{0}</div>", book.Content) };

                using (var client = new SmtpClient())
                {
                    client.CheckCertificateRevocation = false;
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(_emailConfig.Host, _emailConfig.Port, false);
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(emailMessage);
                }
                return "Başarılı!";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
