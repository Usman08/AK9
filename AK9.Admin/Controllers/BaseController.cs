using AK9.AppHelper.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AK9.Admin.Controllers
{
    public class BaseController<T> : Controller
    {
        protected readonly IViewRenderService _viewRenderService;
        protected readonly AppSettings _appSettings;
        protected readonly ILogger<T> _logger;

        public BaseController(IOptions<AppSettings> appSettings, ILogger<T> logger)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        public BaseController(IOptions<AppSettings> appSettings, ILogger<T> logger, IViewRenderService viewRenderService)
        {
            _viewRenderService = viewRenderService;
            _appSettings = appSettings.Value;
            _logger = logger;
        }
    }
}