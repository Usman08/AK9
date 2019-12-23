using Microsoft.AspNetCore.Mvc;

namespace AK9.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}