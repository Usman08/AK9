using AK9.AppHelper.Models;
using AK9.AppHelper.Utils;
using AK9.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace AK9.Web.Controllers
{
    public class CompanyPoliciesController : BaseController<CompanyPoliciesController>
    {
        private readonly IPolicyBLL _policyBLL;

        public CompanyPoliciesController(
            IServiceBLL serviceBLL,
            IPolicyBLL policyBLL,
            IOptions<AppSettings> appSettings,
            ILogger<CompanyPoliciesController> logger
            ) : base(appSettings, logger, serviceBLL)
        {
            _policyBLL = policyBLL;
        }

        public async Task<IActionResult> Index()
        {
            CompanyPolicyModel model = new CompanyPolicyModel
            {
                AppSettings = _appSettings,
                PolicyPdfPath = "{0}/{1}/{2}",
                PolicyList = await _policyBLL.GetListAsync()
            };

            return View(model);
        }
    }
}