using AK9.AppHelper.Models;
using AK9.AppHelper.Utils;
using AK9.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace AK9.Web.Controllers
{
    public class ServiceController : BaseController<ServiceController>
    {
        private readonly IServiceBLL _serviceBLL;

        public ServiceController(
            IServiceBLL serviceBLL,
            IOptions<AppSettings> appSettings,
            ILogger<ServiceController> logger
            ) : base(appSettings, logger, serviceBLL)
        {
            _serviceBLL = serviceBLL;
        }

        public async Task<IActionResult> ServiceDetail(int id)
        {
            ServiceDetailModel model = new ServiceDetailModel()
            {
                ServiceBannerPath = "{0}/{1}/{2}",
                AppSettings = _appSettings,
                Service = await _serviceBLL.GetAsync(id),
                ServiceList = await _serviceBLL.GetListInsteadOfOneAsync(id)

            };

            return View(model);
        }

        private ServiceModel PopulateTestModel(int id)
        {
            ServiceModel model;

            switch (id)
            {
                case 1:
                    model = new ServiceModel
                    {
                        ServiceId = 1,
                        ServiceName = "Security Dog Handler",
                        Heading = "Security Dog Handler"
                    };
                    break;
                case 2:
                    model = new ServiceModel
                    {
                        ServiceId = 1,
                        ServiceName = "Search Dog",
                        Heading = "Search Dog"
                    };
                    break;
                case 3:
                    model = new ServiceModel
                    {
                        ServiceId = 1,
                        ServiceName = "Event Security",
                        Heading = "Event Security"
                    };
                    break;
                case 4:
                    model = new ServiceModel
                    {
                        ServiceId = 1,
                        ServiceName = "Surveillance",
                        Heading = "Surveillance"
                    };
                    break;
                case 5:
                    model = new ServiceModel
                    {
                        ServiceId = 1,
                        ServiceName = "Parks Security",
                        Heading = "Parks Security"
                    };
                    break;
                case 6:
                    model = new ServiceModel
                    {
                        ServiceId = 1,
                        ServiceName = "Close Protection",
                        Heading = "Close Protection"
                    };
                    break;
                case 7:
                    model = new ServiceModel
                    {
                        ServiceId = 1,
                        ServiceName = "Maritime Security",
                        Heading = "Maritime Security"
                    };
                    break;
                case 8:
                    model = new ServiceModel
                    {
                        ServiceId = 1,
                        ServiceName = "Training",
                        Heading = "Training"
                    };
                    break;
                default:
                    model = new ServiceModel
                    {
                        ServiceId = 1,
                        ServiceName = "Security Dog Handler",
                        Heading = "Security Dog Handler"
                    };
                    break;
            }

            return model;
        }
    }
}