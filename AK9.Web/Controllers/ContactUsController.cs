using AK9.AppHelper.AppConstants;
using AK9.AppHelper.Models;
using AK9.AppHelper.Utils;
using AK9.AppHelper.Utils.Notification.EmailHelper;
using AK9.BLL.Services;
using AK9.Web.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AK9.Web.Controllers
{
    public class ContactUsController : BaseController<ContactUsController>
    {
        private IHostingEnvironment _hostingEnvironment;

        public ContactUsController(
            IServiceBLL serviceBLL,
            IOptions<AppSettings> appSettings,
            ILogger<ContactUsController> logger,
            IHostingEnvironment hostingEnvironment
            ) : base(appSettings, logger, serviceBLL)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View(new ContactUsModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<bool> ContactUsEmail(ContactUsModel model)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            string htmlTemplatePath = Path.Combine(_hostingEnvironment.WebRootPath, FolderName.EMAIL_TEMPLATE_FOLDER, EmailTemplate.CONTACT_US_TEMPLATE);
            StringBuilder htmlBody;

            using (StreamReader reader = new StreamReader(htmlTemplatePath))
            {
                htmlBody = new StringBuilder(reader.ReadToEnd());
            }

            htmlBody.Replace(EmailParameter.NAME, model.Name);
            htmlBody.Replace(EmailParameter.PHONE, model.Phone);
            htmlBody.Replace(EmailParameter.EMAIL, model.Email);
            htmlBody.Replace(EmailParameter.SUBJECT, model.Subject);
            htmlBody.Replace(EmailParameter.COMMENTS, model.Comments);

            NotificationUtils<ContactUsController> notificationUtils = new NotificationUtils<ContactUsController>(_appSettings, _logger);
            string subject = string.Format(EmailSubject.CONTACT_US_Subject, model.Subject);

            notificationUtils.SendEmail(subject, htmlBody.ToString(), _appSettings.ToEmail, isBodyHtml: true);
            _logger.LogDebug("ContactUs email is being processed");
            return true;
        }
    }
}