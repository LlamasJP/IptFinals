using IptFinals.Areas.Identity.Data;
using IptFinals.Data;
using IptFinals.Migrations;
using IptFinals.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using AspNetCore.Reporting;
using IptFinals.ViewModels;
using System.Security.Claims;

namespace IptFinals.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IptUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<PersonalInfo> _userManager1;
        private readonly IptDbContext _context;

        public IdentificationModel identification = new IdentificationModel();

        public HomeController(ILogger<HomeController> logger, UserManager<IptUser> userManager, IWebHostEnvironment webHostEnvironment, IptDbContext context)
        {
            _logger = logger;
            this._userManager = userManager;
            this._webHostEnvironment = webHostEnvironment;
            _context = context;

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public IActionResult Index()
        {
            ViewData["UserID"] = _userManager.GetUserId(this.User);
            return View();
        }

        public async Task<IActionResult> Identification(string id)
        {
           
            if (User.IsInRole("Manager"))
            {
                return View();
               
            }
            else
            {
                
                var personalInfo = await _context.PersonalInfo
                      .FirstOrDefaultAsync(m => m.UserId == id);
                if (personalInfo == null)
                {
                    return RedirectToAction("Create", "PersonalInfo");
                }
                else
                {
                    ViewData["UserID"] = _userManager.GetUserId(this.User);
                    return View(personalInfo);
                }
            }
  
        }

        public IActionResult Identifications(string studentid, string firstname, string lastname, string section, string course, string yearlevel, string address, string contactnumber, string emergencycontact, string dateofbirth)
        {
            string id = studentid;
            string fname = firstname;
            string lname = lastname;
            string sect = section;
            string crs = course;
            string yl = yearlevel;
            string add = address;
            string contactnum = contactnumber;
            string econtact = emergencycontact;
            string dob = dateofbirth;

            string mimeType = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\rptStudentID.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("studIDNumber", id);
            parameters.Add("studFirstName", fname);
            parameters.Add("studLastName", lname);
            parameters.Add("studDateOfBirth", dob);
            parameters.Add("studAddress", add);
            parameters.Add("EmergencyContactNumber", contactnum);
            parameters.Add("studEmergencyContact", econtact);
            LocalReport localReport = new LocalReport(path);
            var res = localReport.Execute(RenderType.Pdf, extension, parameters, mimeType);
            return File(res.MainStream, "application/pdf");
        }
    }
}