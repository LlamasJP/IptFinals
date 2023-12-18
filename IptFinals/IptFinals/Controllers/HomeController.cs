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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;
using System.Drawing.Imaging;


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
                var personalInfo = await _context.PersonalInfo
                     .FirstOrDefaultAsync(m => m.PersonalId == id);
                if (personalInfo == null)
                {
                    return RedirectToAction("Index", "PersonalInfo");
                }
                else
                {
                    return View(personalInfo);
                }
                  


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



        public IActionResult Identifications(string studentid, string firstname, string lastname, string section, string course, string yearlevel, string address, string contactnumber, string emergencycontact, string dateofbirth, string studImage)
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


            string Imageparam = "";
           
            var imagepath = $"{this._webHostEnvironment.WebRootPath}\\images\\" + studImage;

            using (var b = new Bitmap(imagepath))
            {
                using (var ms = new MemoryStream())
                {
                    b.Save(ms, ImageFormat.Bmp);
                    Imageparam = Convert.ToBase64String(ms.ToArray());
                }
            }

            string mimeType = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\rptStudentID.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("studIDNumber", id);
            parameters.Add("studFirstName", fname);
            parameters.Add("studLastName", lname);
            parameters.Add("studDateOfBirth", dob);
            parameters.Add("studAddress", add);
            parameters.Add("studCourse", crs);
            parameters.Add("EmergencyContactNumber", contactnum);
            parameters.Add("studEmergencyContact", econtact);
            parameters.Add("studImage", Imageparam);
            LocalReport localReport = new LocalReport(path);
            var res = localReport.Execute(RenderType.Pdf, extension, parameters, mimeType);
            return File(res.MainStream, "application/pdf");
        }

      

        public async Task Login()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Register");
        }

  
    }
}