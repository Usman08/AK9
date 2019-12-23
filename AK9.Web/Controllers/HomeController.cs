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
    public class HomeController : BaseController<HomeController>
    {
        private readonly IServiceBLL _serviceBLL;
        private readonly ICertificationBLL _certificationBLL;
        private IHostingEnvironment _hostingEnvironment;

        public HomeController(
            IServiceBLL serviceBLL,
            ICertificationBLL certificationBLL,
            IOptions<AppSettings> appSettings,
            ILogger<HomeController> logger,
            IHostingEnvironment hostingEnvironment
            ) : base(appSettings, logger, serviceBLL)
        {
            _serviceBLL = serviceBLL;
            _certificationBLL = certificationBLL;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            //TestEmail();
            HomeModel model = new HomeModel
            {
                AppSettings = _appSettings,
                CertificationLogoPath = "{0}/{1}/{2}",
                AskAQuote = new AskAQuoteModel(),
                CertificationList = await _certificationBLL.GetListAsync(),
                ServiceList = await _serviceBLL.GetListAsync()
            };

            return View(model);
        }

        public void TestEmail()
        {
            NotificationUtils<HomeController> notificationUtils = new NotificationUtils<HomeController>(_appSettings, _logger);

            notificationUtils.SendEmail("Testing Email", "Testing Body", _appSettings.TestEmail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<bool> AskAQuoteEmail(AskAQuoteModel model)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            string htmlTemplatePath = Path.Combine(_hostingEnvironment.WebRootPath, FolderName.EMAIL_TEMPLATE_FOLDER, EmailTemplate.ASK_QUOTE_TEMPLATE);
            StringBuilder htmlBody;

            using (StreamReader reader = new StreamReader(htmlTemplatePath))
            {
                htmlBody = new StringBuilder(reader.ReadToEnd());
            }

            htmlBody.Replace(EmailParameter.NAME, model.FullName);
            htmlBody.Replace(EmailParameter.COMPANY, model.Company);
            htmlBody.Replace(EmailParameter.PHONE, model.Phone);
            htmlBody.Replace(EmailParameter.EMAIL, model.Email);
            htmlBody.Replace(EmailParameter.ADDRESS, model.Address);
            htmlBody.Replace(EmailParameter.CITYSTATEZIP, model.CityStateZip);
            htmlBody.Replace(EmailParameter.DESCRIPTION, model.Description);

            NotificationUtils<HomeController> notificationUtils = new NotificationUtils<HomeController>(_appSettings, _logger);
            string subject = string.Format(EmailSubject.ASK_QUOTE_Subject, model.FullName);

            notificationUtils.SendEmail(subject, htmlBody.ToString(), _appSettings.ToEmail, isBodyHtml: true);
            _logger.LogDebug("AskAQuote email is being processed");
            return true;
        }
    }
}
