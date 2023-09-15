using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class MailSender : IMailSender
    {
        private readonly IConfiguration _config;

        public MailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendToOne(string to, string subject, string body, bool isHtml, CancellationToken token)
        {
            var config = _config.GetSection("SmtpClient");
            var host = config.GetValue<string>("Host");
            var port = config.GetValue<int>("Port");
            var login = config.GetValue<string>("Login");
            var password = config.GetValue<string>("Password");
            var from = config.GetValue<string>("From");

            using (var client = new SmtpClient())
            {
                client.Host = host!;
                client.Port = port;
                client.Credentials = new NetworkCredential(login, password);
                client.EnableSsl = false;
                client.UseDefaultCredentials = false;

                var mail = new MailMessage(from!, to, subject, body);
                mail.IsBodyHtml = isHtml;

                try
                {
                    await client.SendMailAsync(mail, token);
                } catch(SmtpException ex)
                {
                    // TODO: log
                }
            }
        }
    }
}
