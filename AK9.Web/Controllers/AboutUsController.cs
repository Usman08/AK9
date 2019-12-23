using AK9.AppHelper.Utils;
using AK9.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AK9.Web.Controllers
{
    public class AboutUsController : BaseController<AboutUsController>
    {
        public AboutUsController(
            IServiceBLL serviceBLL,
            IOptions<AppSettings> appSettings,
            ILogger<AboutUsController> logger
            ) : base(appSettings, logger, serviceBLL)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}