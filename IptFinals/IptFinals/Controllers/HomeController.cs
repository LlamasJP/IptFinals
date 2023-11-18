using IptFinals.Areas.Identity.Data;
using IptFinals.Migrations;
using IptFinals.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace IptFinals.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IptUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<PersonalInfo> _userManager1;

        public  Identification identification=new Identification();

        public HomeController(ILogger<HomeController> logger, UserManager<IptUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            this._userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            ViewData["UserID"] = _userManager.GetUserId(this.User);
            //var UserID = _userManager1.GetUserId(HttpContext.User);
            //PersonalInfo personalinfo = _userManager1.FindByIdAsync(UserID).Result;

            return View();
        }

        public IActionResult PersonalInfo()
        {
            //ViewData["UserID"] = _userManager.GetUserId(this.User);
            return View();
        }
        public IActionResult Identification()
        {
            ViewData["UserID"] = _userManager.GetUserId(this.User);
            //ViewData["UserID"] = _userManager.GetUserId(this.User);
            return View();
        }
        public IActionResult AccountSetting()
        {
            //    ViewData["UserID"] = _userManager.GetUserId(this.User);
            return View();
        }
        //public IActionResult StudentPersonalInfo()
        //{
        //    var dt = new DataTable();
        //    dt = identification.GetStudentPersonalInfo();

        //    string mimeType = "";
        //    int extension = 1;
        //    var path = $"{_webHostEnvironment.WebRootPath}\\Reports\\rptStudentPersonalInfo.rdlc";

        //}
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}