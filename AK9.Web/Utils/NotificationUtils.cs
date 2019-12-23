using AK9.AppHelper.Utils;
using AK9.AppHelper.Utils.Notification;
using Microsoft.Extensions.Logging;

namespace AK9.Web.Utils
{
    public class NotificationUtils<T>
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<T> _logger;

        public NotificationUtils(AppSettings appSettings, ILogger<T> logger)
        {
            _appSettings = appSettings;
            _logger = logger;
        }

        public void SendEmail(string subject, string body, string emailTo, string emailCC = "", bool isBodyHtml = false)
        {
            Office365EmailNotification email = new Office365EmailNotification
            {
                EmailCredential = new EmailCredential()
                {
                    SmtpServer = _appSettings.SmtpServer,
                    Username = _appSettings.SmtpUsername,
                    Password = _appSettings.SmtpPassword,
                    Port = _appSettings.SmtpPort,
                    EnableSSL = _appSettings.SmtpSSL
                }
            };

            EmailMessage emailMessage = new EmailMessage
            {
                Subject = _appSettings.IsTestEmail ? string.Format("Test - {0}", subject) : subject,
                ToEmail = _appSettings.IsTestEmail ? _appSettings.TestEmail : emailTo,
                CCEmail = emailCC,
                FromEmail = _appSettings.FromEmail,
                Body = body,
                IsBodyHtml = isBodyHtml
            };

            //email.SendCompleted -= Email_SendCompleted;
            //email.SendCompleted += Email_SendCompleted;

            email.SendAsync(emailMessage);
        }

        private void Email_SendCompleted(object sender, SendCompletedArgs e)
        {
            if (e.Cancelled)
            {
                _logger.LogError(e.Error.Message, e);
            }
            else
            {
                _logger.LogDebug("Email sent successfully.");
            }
        }
    }
}
