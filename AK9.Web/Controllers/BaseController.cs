using AK9.AppHelper.AppConstants;
using AK9.AppHelper.Models;
using AK9.AppHelper.Utils;
using AK9.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AK9.Web.Controllers
{
    public class BaseController<T> : Controller
    {
        protected readonly AppSettings _appSettings;
        protected readonly ILogger<T> _logger;
        private readonly IServiceBLL _serviceBLL;

        public BaseController(IOptions<AppSettings> appSettings, ILogger<T> logger, IServiceBLL serviceBLL)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
            _serviceBLL = serviceBLL;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Task<List<ServiceModel>> task = Task.Run<List<ServiceModel>>(async () => await _serviceBLL.GetListAsync());

            ViewData.SetViewData(task.Result, HelpingVariable.SERVICE_MENU);
            base.OnActionExecuted(context);
        }
    }
}