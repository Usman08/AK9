using Microsoft.AspNetCore.Mvc;

namespace AK9.Admin.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}