namespace AK9.AppHelper.Utils
{
    public class AppSettings
    {
        public string SmtpServer { get; set; }

        public string FromEmail { get; set; }

        public int SmtpPort { get; set; }

        public string SmtpUsername { get; set; }

        public string SmtpPassword { get; set; }

        public bool SmtpSSL { get; set; }

        public bool IsTestEmail { get; set; }

        public string TestEmail { get; set; }

        public string AdminUrl { get; set; }

        public string ToEmail { get; set; }
    }
}
