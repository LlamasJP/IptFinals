using Microsoft.AspNetCore.Mvc;

namespace IptFinals.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Print()
        {
            return View();
        }
    }
}
