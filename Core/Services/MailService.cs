using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class MailService : IMailService
    {
        private readonly IMailSender _mailSender;

        public MailService(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }

        public async Task SendUserProfileActivate(string email, string activateToken)
        {
            string link = $"http://localhost:7076/api/Auth/Activate/{activateToken}";

            string subject = "Account activation";
            string body = $"To start using your account, please activate it by following link: <br /><br />" +
                       $"{link}";

            await _mailSender.SendToOne(email, subject, body, true, new CancellationToken());
        }

        public async Task SendEmailChangeConfirm(string email, string activateToken)
        {
            string link = $"http://localhost:7076/api/Auth/ConfirmEmail/{activateToken}";

            string subject = "Confirming email changes";
            string body = $"To confirm the email change, please click on the following link: <br /><br />" +
                       $"{link}";

            await _mailSender.SendToOne(email, subject, body, true, new CancellationToken());
        }

        public async Task SendRecoveryPassword(string email, string recoveryToken, CancellationToken token)
        {
            string link = $"http://localhost:7076/api/Auth/RecoveryPassword/{recoveryToken}";

            string subject = "Recovery password";
            string body = $"To reset your password, please click on the following link or copy and paste it into your browser's address bar: <br /><br />" +
                       $"{link}";

            await _mailSender.SendToOne(email, subject, body, true, new CancellationToken());
        }

    }
}
