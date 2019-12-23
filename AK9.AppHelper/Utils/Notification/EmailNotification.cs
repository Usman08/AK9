using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace AK9.AppHelper.Utils.Notification
{
    public class EmailNotification : INotification
    {
        public EmailCredential EmailCredential { get; set; }

        public event EventHandler<SendCompletedArgs> SendCompleted;

        public EmailNotification()
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

            SmtpClient smtpServer = new SmtpClient(EmailCredential.SmtpServer)
            {
                Port = EmailCredential.Port,
                Credentials = new NetworkCredential(EmailCredential.Username, EmailCredential.Password),
                EnableSsl = EmailCredential.EnableSSL
            };

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(emailMessage.FromEmail),
                Subject = (!string.IsNullOrEmpty(emailMessage.Subject) ? emailMessage.Subject : ""),
                Body = (!string.IsNullOrEmpty(emailMessage.Body) ? emailMessage.Body : ""),
                IsBodyHtml = emailMessage.IsBodyHtml
            };

            mail.To.Add(emailMessage.ToEmail);

            if (!string.IsNullOrEmpty(emailMessage.CCEmail))
            {
                mail.CC.Add(emailMessage.CCEmail);
            }

            smtpServer.SendCompleted -= SmtpServer_SendCompleted;
            smtpServer.SendCompleted += SmtpServer_SendCompleted;

            smtpServer.SendAsync(mail, cancellationToken);
        }

        private void SmtpServer_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            SendCompleted(sender, new SendCompletedArgs(e.Error, e.Cancelled, e.UserState));
        }

        private List<string> GetEmails(string emails)
        {
            return emails.Split(',').ToList();
        }
    }

    public class SendCompletedArgs : AsyncCompletedEventArgs
    {
        public SendCompletedArgs(Exception error, bool cancelled, object userState) : base(error, cancelled, userState)
        {
        }
    }
}
