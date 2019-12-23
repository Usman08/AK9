namespace AK9.AppHelper.Utils.Notification.EmailHelper
{
    public class EmailTemplate
    {
        public const string CONTACT_US_TEMPLATE = "contact-us.html";
        public const string ASK_QUOTE_TEMPLATE = "ask-a-quote.html";
    }

    public class EmailSubject
    {
        public const string CONTACT_US_Subject = "ContactUs: {0}";
        public const string ASK_QUOTE_Subject = "AskAQuote: {0}";
    }

    public class EmailParameter
    {
        public const string NAME = "{Name}";
        public const string PHONE = "{Phone}";
        public const string EMAIL = "{Email}";
        public const string ADDRESS = "{Address}";
        public const string CITYSTATEZIP = "{CityStateZip}";
        public const string COMMENTS = "{Comments}";
        public const string DESCRIPTION = "{Description}";
        public const string COMPANY = "{Company}";
        public const string SUBJECT = "{Subject}";
    }
}
