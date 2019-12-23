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
    public class PolicyController : BaseController<PolicyController>
    {
        private readonly IPolicyBLL _policyBLL;
        private IHostingEnvironment _hostingEnvironment;

        public PolicyController(
            IPolicyBLL policyBLL,
            IHostingEnvironment hostingEnvironment,
            IOptions<AppSettings> appSettings,
            IViewRenderService viewRenderService,
            ILogger<PolicyController> logger
            ) : base(appSettings, logger, viewRenderService)
        {
            _policyBLL = policyBLL;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            PolicyListModel model = new PolicyListModel();
            model.PolicyList = await _policyBLL.GetListAsync();
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            return View(await Task.Run(() => { return new PolicyModel(); }));
        }

        [HttpPost]
        public async Task<IActionResult> Create(PolicyModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (await _policyBLL.SaveAsync(model) > 0)
            {
                _logger.LogDebug("Policy created successfully.", model.PolicyId);
                TempData.SetStatus(new StatusModel { TransactionStatus = StatusEnum.Succeed, StatusMessage = string.Format(Message.CREATE_SUCCESS, "Policy") });
                string savedFileName = string.Empty;

                if (Request.Form.Files != null && Request.Form.Files.Count > 0)
                {
                    string path = Path.Combine(_hostingEnvironment.WebRootPath, FolderName.POLICY_PDF_FOLDER);
                    savedFileName = Helper.UploadImage(Request.Form.Files[0], path, model.PolicyId.ToString());
                    _logger.LogDebug("Policy file uploaded successfully.", model.PolicyId);
                    model.PolicyFile = savedFileName;

                    await _policyBLL.UpdateAsync(model);
                    _logger.LogDebug("Policy file name updated successfully.", model.PolicyId);
                }
            }
            else
            {
                _logger.LogDebug("Policy creation failed.");
                TempData.SetStatus(new StatusModel { TransactionStatus = StatusEnum.Failed, StatusMessage = string.Format(Message.CREATE_FAILURE, "policy") });
            }

            return RedirectToAction("Update", new { id = model.PolicyId });
        }

        public async Task<IActionResult> Update(int id)
        {
            if (TempData[HelpingVariable.STATUS] != null)
            {
                ViewData.SetViewData(TempData.GetStatus(), HelpingVariable.STATUS);
            }

            return View(await _policyBLL.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(PolicyModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            if (Request.Form.Files != null && Request.Form.Files.Count > 0)
            {
                string savedFileName = string.Empty;
                string path = Path.Combine(_hostingEnvironment.WebRootPath, FolderName.POLICY_PDF_FOLDER);
                savedFileName = Helper.UploadImage(Request.Form.Files[0], path, model.PolicyId.ToString());
                _logger.LogDebug("Policy file updated successfully.", model.PolicyId);
                model.PolicyFile = savedFileName;
            }

            if ((await _policyBLL.UpdateAsync(model)) > 0)
            {
                _logger.LogDebug("Policy updated successfully.", model.PolicyId);
                ViewData.SetViewData(new StatusModel { TransactionStatus = StatusEnum.Succeed, StatusMessage = string.Format(Message.UPDATE_SUCCESS, "Policy") }, HelpingVariable.STATUS);
            }
            else
            {
                _logger.LogDebug("Policy updation failed.", model.PolicyId);
                ViewData.SetViewData(new StatusModel { TransactionStatus = StatusEnum.Failed, StatusMessage = string.Format(Message.UPDATE_FAILURE, "policy") }, HelpingVariable.STATUS);
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (await _policyBLL.DeleteAsync(id) > 0)
            {
                _logger.LogDebug("Policy deleted successfully.", id);
                List<PolicyModel> lstPolicy = await _policyBLL.GetListAsync();
                string htmlString = await _viewRenderService.RenderToStringAsync("Policy/_PolicyList", lstPolicy, true);
                return Json(new { status = StatusEnum.Succeed.ToString(), view = htmlString });
            }
            else
            {
                _logger.LogDebug("Policy deletion failed.", id);
                return Json(new { status = StatusEnum.Failed.ToString(), view = "" });
            }
        }
    }
}