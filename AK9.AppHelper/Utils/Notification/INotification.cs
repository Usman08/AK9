using System.Threading;

namespace AK9.AppHelper.Utils.Notification
{
    public interface INotification
    {
        void SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default(CancellationToken));
    }

    public class EmailCredential
    {
        public string SmtpServer { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
    }

    public class EmailMessage
    {
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string CCEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}
