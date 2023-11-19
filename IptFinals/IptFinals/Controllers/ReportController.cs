using AspNetCore.Reporting;
using IptFinals.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IptFinals.Controllers
{
    public class ReportController : Controller
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Identification identification = new Identification();
        public ReportController(ILogger<ReportController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
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
