using AspNetCore.Reporting;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using IptFinals.Areas.Identity.Data;
using IptFinals.Data;
using IptFinals.Migrations;
using IptFinals.Models;
using IptFinals.ReportDataSet;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
using System.Collections.Immutable;
using System.Data;
using System.Data.SqlClient;
using IptFinals.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IptFinals.Controllers
{
    public class ReportController : Controller
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IptDbContext _context;
        //private readonly UserManager<PersonalInfo> _userManager;

        public IdentificationModel identification = new IdentificationModel();
        public string PersonalId { get; set; }
        public ReportController(ILogger<ReportController> logger, IWebHostEnvironment webHostEnvironment, IptDbContext context /*UserManager<PersonalInfo> userManager*/)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            //this._userManager = userManager;

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
        public IActionResult Index()
        {
            return View();
        }
    

    public IActionResult Print()
        {
            var dt = new DataTable();
            dt = identification.GetStudentPersonalInfo();

            string mimeType = "";
            int extension = 1;
            var path = $"{_webHostEnvironment.WebRootPath}\\Reports\\rptStudentPersonalInfo.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm1", "RDLC Report");
            parameters.Add("prm2",DateTime.Now.ToString("dd-MMM-yyyy"));
            parameters.Add("prm3", "Student Personal Info Report"); 

            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsStudentPersonalInfo", dt);

            var res = localReport.Execute(RenderType.Pdf, extension, parameters, mimeType);
            return File(res.MainStream, "application/pdf");
            //return View();
        }
    }
}
