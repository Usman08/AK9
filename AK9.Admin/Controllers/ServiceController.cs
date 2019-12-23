using AK9.AppHelper.AppConstants;
using AK9.AppHelper.Enums;
using AK9.AppHelper.Models;
using AK9.AppHelper.Utils;
using AK9.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AK9.Admin.Controllers
{
    [Authorize]
    public class ServiceController : BaseController<ServiceController>
    {
        private readonly IServiceBLL _serviceBLL;
        private IHostingEnvironment _hostingEnvironment;

        public ServiceController(
            IServiceBLL serviceBLL,
            IHostingEnvironment hostingEnvironment,
            IOptions<AppSettings> appSettings,
            IViewRenderService viewRenderService,
            ILogger<ServiceController> logger
            ) : base(appSettings, logger, viewRenderService)
        {
            _serviceBLL = serviceBLL;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            ServiceListModel model = new ServiceListModel();
            model.ServiceList = await _serviceBLL.GetListAsync();
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            ServiceModel model = new ServiceModel();
            model.ServiceIconSelectList = await _serviceBLL.PopulateServiceIconList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (await _serviceBLL.SaveAsync(model) > 0)
            {
                _logger.LogDebug("Service created successfully.", model.ServiceId);
                TempData.SetStatus(new StatusModel { TransactionStatus = StatusEnum.Succeed, StatusMessage = string.Format(Message.CREATE_SUCCESS, "Service") });

                string savedFileName = string.Empty;

                if (Request.Form.Files != null && Request.Form.Files.Count > 0)
                {
                    string path = Path.Combine(_hostingEnvironment.WebRootPath, FolderName.SERVICE_BANNER_IMAGE_FOLDER);
                    savedFileName = Helper.UploadImage(Request.Form.Files[0], path, model.ServiceId.ToString());
                    _logger.LogDebug("Service banner image uploaded successfully.", model.ServiceId);
                    model.BannerImage = savedFileName;

                    await _serviceBLL.UpdateAsync(model);
                    _logger.LogDebug("Service banner image name updated successfully.", model.ServiceId);
                }
            }
            else
            {
                _logger.LogDebug("Service creation failed.");
                TempData.SetStatus(new StatusModel { TransactionStatus = StatusEnum.Failed, StatusMessage = string.Format(Message.CREATE_FAILURE, "service") });
            }

            return RedirectToAction("Update", new { id = model.ServiceId });
        }

        public async Task<IActionResult> Update(int id)
        {
            if (TempData[HelpingVariable.STATUS] != null)
            {
                ViewData.SetViewData(TempData.GetStatus(), HelpingVariable.STATUS);
            }

            ServiceModel model = await _serviceBLL.GetAsync(id);

            if (model != null)
                model.ServiceIconSelectList = await _serviceBLL.PopulateServiceIconList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ServiceModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            if (Request.Form.Files != null && Request.Form.Files.Count > 0)
            {
                string savedFileName = string.Empty;
                string path = Path.Combine(_hostingEnvironment.WebRootPath, FolderName.SERVICE_BANNER_IMAGE_FOLDER);
                savedFileName = Helper.UploadImage(Request.Form.Files[0], path, model.ServiceId.ToString());
                _logger.LogDebug("Service banner image updated successfully.", model.ServiceId);
                model.BannerImage = savedFileName;
            }

            if ((await _serviceBLL.UpdateAsync(model)) > 0)
            {
                _logger.LogDebug("Service updated successfully.", model.ServiceId);
                ViewData.SetViewData(new StatusModel { TransactionStatus = StatusEnum.Succeed, StatusMessage = string.Format(Message.UPDATE_SUCCESS, "Service") }, HelpingVariable.STATUS);
            }
            else
            {
                _logger.LogDebug("Service updation failed.", model.ServiceId);
                ViewData.SetViewData(new StatusModel { TransactionStatus = StatusEnum.Failed, StatusMessage = string.Format(Message.UPDATE_FAILURE, "service") }, HelpingVariable.STATUS);
            }

            model.ServiceIconSelectList = await _serviceBLL.PopulateServiceIconList();

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _serviceBLL.DeleteAsync(id) > 0)
            {
                _logger.LogDebug("Service deleted successfully.", id);
                List<ServiceModel> lstService = await _serviceBLL.GetListAsync();
                string htmlString = await _viewRenderService.RenderToStringAsync("Service/_ServiceList", lstService, true);
                return Json(new { status = StatusEnum.Succeed.ToString(), view = htmlString });
            }
            else
            {
                _logger.LogDebug("Service deletion failed.", id);
                return Json(new { status = StatusEnum.Failed.ToString(), view = "" });
            }
        }
    }
}