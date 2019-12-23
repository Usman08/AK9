using EASendMail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace AK9.AppHelper.Utils.Notification
{
    public class Office365EmailNotification
    {
        public EmailCredential EmailCredential { get; set; }

        public event EventHandler<SendCompletedArgs> SendCompleted;

        public Office365EmailNotification()
        {

        }

        public void SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (EmailCredential == null)
            {
                throw new Exception("No email credentials found.");
            }

            if (string.IsNullOrEmpty(emailMessage.ToEmail))
            {
                throw new Exception("No email recepient found.");
            }

            SmtpServer smtpServer = new SmtpServer(EmailCredential.SmtpServer)
            {
                Port = EmailCredential.Port,
                User = EmailCredential.Username,
                Password = EmailCredential.Password,
                ConnectType = SmtpConnectType.ConnectSSLAuto
            };

            SmtpMail smtpMail = new SmtpMail("TryIt")
            {
                From = emailMessage.FromEmail,
                Subject = (!string.IsNullOrEmpty(emailMessage.Subject) ? emailMessage.Subject : "")
            };

            if (emailMessage.IsBodyHtml)
            {
                smtpMail.HtmlBody = (!string.IsNullOrEmpty(emailMessage.Body) ? emailMessage.Body : "");
            }
            else
            {
                smtpMail.TextBody = (!string.IsNullOrEmpty(emailMessage.Body) ? emailMessage.Body : "");
            }

            smtpMail.To.AddRange(GetEmails(emailMessage.ToEmail));

            if (!string.IsNullOrEmpty(emailMessage.CCEmail))
            {
                smtpMail.Cc.AddRange(GetEmails(emailMessage.CCEmail));
            }

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.SendMail(smtpServer, smtpMail);
        }

        private IEnumerable<MailAddress> GetEmails(string emails)
        {
            return emails.Split(',').Select(i => new MailAddress(i)).ToList();
        }
    }
    
}
