using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IptFinals.Data;
using IptFinals.Models;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using IptFinals.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using IptFinals.Migrations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using System.Data.SqlClient;
using AspNetCore.Reporting.ReportExecutionService;

namespace IptFinals.Controllers
{
    public class PersonalInfoController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly ILogger<PersonalInfoController> _logger;
        private readonly UserManager<IptUser> _userManager;
        private readonly IptDbContext _context;

        string constr = "Data Source=DESKTOP-FAE0OA1; Initial Catalog=IptFinalsDB;User ID=sa; Password=123456789";

        public PersonalInfo personalinfo = new PersonalInfo();

        public PersonalInfoController(IptDbContext context, UserManager<IptUser> userManager, IWebHostEnvironment webHostEnvironment) //* ILogger<PersonalInfoController> logger,*, IWebHostEnvironment webHostEnvironment)
        {
            //_logger = logger;
            this._userManager = userManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Index()
        {
              return _context.PersonalInfo != null ? 
                          View(await _context.PersonalInfo.ToListAsync()) :
                          Problem("Entity set 'IptDbContext.PersonalInfo'  is null.");
        }

        // GET: PersonalInfo/Details/5
        public async Task<IActionResult> Details(string id)
        {
           
            if (User.IsInRole("Manager"))
            {
                if (id == null || _context.PersonalInfo == null)
                {
                    return NotFound("no user");
                }

                var personalInfo = await _context.PersonalInfo
                    .FirstOrDefaultAsync(m => m.PersonalId == id);
                if (personalInfo == null)
                {
                    return NotFound("Hello");
                }
                return View(personalInfo);
            } 
            else
            {
                ViewData["UserID"] = _userManager.GetUserId(this.User);
                var personalInfo = await _context.PersonalInfo
                   .FirstOrDefaultAsync(m => m.UserId == id);
                if (personalInfo == null)
                {
                    return RedirectToAction("Create", "PersonalInfo");
                }
                else
                return View(personalInfo);
            }    
        }
        public IActionResult Create()
        {

            ViewData["UserID"] = _userManager.GetUserId(this.User);
            return View();
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonalId,,StudentId,UserId,FirstName,LastName,Section,Course,YearLevel,ContactNumber,DateOfBirth,Address,EmergencyContact")] PersonalInfo personalInfo)
        {
             if (ModelState.IsValid)
            {
                _context.Add(personalInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personalInfo);
        }



        public async Task<IActionResult> Edit(string id)
        {

            if (User.IsInRole("Manager")) {
                if (id == null || _context.PersonalInfo == null)
                {
                    return NotFound("No UserId");
                }

                var personalInfo = await _context.PersonalInfo.FindAsync(id);
                if (personalInfo == null)
                {
                    return NotFound("no user");
                }
                return View(personalInfo);
            }
            else
            {
                ViewData["UserID"] = _userManager.GetUserId(this.User);
                if (id == null || _context.PersonalInfo == null)
                {
                    return NotFound("No UserId");
                }

                var personalInfo = await _context.PersonalInfo.FindAsync(id);
                if (personalInfo == null)
                {
                    return NotFound("no user");
                }
                return View(personalInfo);
            }

            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StudentId,PersonalId,UserId,FirstName,LastName,Section,Course,YearLevel,Address,DateOfBirth,ContactNumber,EmergencyContact")] PersonalInfo personalInfo)
        {
            
            if (User.IsInRole("Manager"))
            {
                if (id != personalInfo.PersonalId)
                {
                    return NotFound("No user");
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(personalInfo);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PersonalInfoExists(personalInfo.PersonalId))
                        {
                            return NotFound("Success");
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            } 
            else
            {

                ViewData["UserID"] = _userManager.GetUserId(this.User);
                if (id != personalInfo.PersonalId)
                {
                    return NotFound("No user");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(personalInfo);
                        await _context.SaveChangesAsync();
                       
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PersonalInfoExists(personalInfo.PersonalId))
                        {
                            return NotFound("Success");
                        }
                        else
                        {
                            throw;
                        }
                    }
                    //ViewBag.Message = "Successfully Change";
                    return RedirectToAction(nameof(Edit));
                    
                }
            }
            return View(personalInfo);
        }

        // GET: PersonalInfo/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.PersonalInfo == null)
            {
                return NotFound();
            }

            var personalInfo = await _context.PersonalInfo
                .FirstOrDefaultAsync(m => m.PersonalId == id);
            if (personalInfo == null)
            {
                return NotFound();
            }

            return View(personalInfo);
        }

        // POST: PersonalInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.PersonalInfo == null)
            {
                return Problem("Entity set 'IptDbContext.PersonalInfo'  is null.");
            }
            var personalInfo = await _context.PersonalInfo.FindAsync(id);
            if (personalInfo != null)
            {
                _context.PersonalInfo.Remove(personalInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalInfoExists(string id)
        {
          return (_context.PersonalInfo?.Any(e => e.PersonalId == id)).GetValueOrDefault();
        }
    }
}
