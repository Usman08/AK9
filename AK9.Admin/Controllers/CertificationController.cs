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
    public class CertificationController : BaseController<CertificationController>
    {
        private readonly ICertificationBLL _certificationBLL;
        private IHostingEnvironment _hostingEnvironment;

        public CertificationController(
            ICertificationBLL certificationBLL,
            IHostingEnvironment hostingEnvironment,
            IOptions<AppSettings> appSettings,
            IViewRenderService viewRenderService,
            ILogger<CertificationController> logger
            ) : base(appSettings, logger, viewRenderService)
        {
            _certificationBLL = certificationBLL;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            CertificationListModel model = new CertificationListModel();
            model.CertificationList = await _certificationBLL.GetListAsync();
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            return View(await Task.Run(() => { return new CertificationModel(); }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CertificationModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (await _certificationBLL.SaveAsync(model) > 0)
            {
                _logger.LogDebug("Certification created successfully.", model.CertificationId);
                TempData.SetStatus(new StatusModel { TransactionStatus = StatusEnum.Succeed, StatusMessage = string.Format(Message.CREATE_SUCCESS, "Certification") });
                string savedFileName = string.Empty;

                if (Request.Form.Files != null && Request.Form.Files.Count > 0)
                {
                    string path = Path.Combine(_hostingEnvironment.WebRootPath, FolderName.CERTIFICATION_IMAGE_FOLDER);
                    savedFileName = Helper.UploadImage(Request.Form.Files[0], path, model.CertificationId.ToString());
                    _logger.LogDebug("Certification logo uploaded successfully.", model.CertificationId);
                    model.CertificationImage = savedFileName;

                    await _certificationBLL.UpdateAsync(model);
                    _logger.LogDebug("Certification logo name updated successfully.", model.CertificationId);
                }
            }
            else
            {
                _logger.LogDebug("Certification creation failed.");
                TempData.SetStatus(new StatusModel { TransactionStatus = StatusEnum.Failed, StatusMessage = string.Format(Message.CREATE_FAILURE, "certification") });
            }

            return RedirectToAction("Update", new { id = model.CertificationId });
        }

        public async Task<IActionResult> Update(int id)
        {
            if (TempData[HelpingVariable.STATUS] != null)
            {
                ViewData.SetViewData(TempData.GetStatus(), HelpingVariable.STATUS);
            }

            return View(await _certificationBLL.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(CertificationModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            if (Request.Form.Files != null && Request.Form.Files.Count > 0)
            {
                string savedFileName = string.Empty;
                string path = Path.Combine(_hostingEnvironment.WebRootPath, FolderName.CERTIFICATION_IMAGE_FOLDER);
                savedFileName = Helper.UploadImage(Request.Form.Files[0], path, model.CertificationId.ToString());
                _logger.LogDebug("Certification logo updated successfully.", model.CertificationId);
                model.CertificationImage = savedFileName;
            }

            if ((await _certificationBLL.UpdateAsync(model)) > 0)
            {
                _logger.LogDebug("Certification updated successfully.", model.CertificationId);
                ViewData.SetViewData(new StatusModel { TransactionStatus = StatusEnum.Succeed, StatusMessage = string.Format(Message.UPDATE_SUCCESS, "Certification") }, HelpingVariable.STATUS);
            }
            else
            {
                _logger.LogDebug("Certification updation failed.", model.CertificationId);
                ViewData.SetViewData(new StatusModel { TransactionStatus = StatusEnum.Failed, StatusMessage = string.Format(Message.UPDATE_FAILURE, "certification") }, HelpingVariable.STATUS);
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _certificationBLL.DeleteAsync(id) > 0)
            {
                _logger.LogDebug("Certification deleted successfully.", id);
                List<CertificationModel> lstCertification = await _certificationBLL.GetListAsync();
                string htmlString = await _viewRenderService.RenderToStringAsync("Certification/_CertificationList", lstCertification, true);
                return Json(new { status = StatusEnum.Succeed.ToString(), view = htmlString });
            }
            else
            {
                _logger.LogDebug("Certification deletion failed.", id);
                return Json(new { status = StatusEnum.Failed.ToString(), view = "" });
            }
        }
    }
}