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

namespace IptFinals.Controllers
{
    public class PersonalInfoController : Controller
    {
        //private readonly ILogger<PersonalInfoController> _logger;
        private readonly UserManager<IptUser> _userManager;
        //private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IptDbContext _context;

        public PersonalInfoController(IptDbContext context, UserManager<IptUser> userManager) //* ILogger<PersonalInfoController> logger,*, IWebHostEnvironment webHostEnvironment)
        {
            //_logger = logger;
            this._userManager = userManager;
            //_webHostEnvironment = webHostEnvironment;
            _context = context;
        }
        //public PersonalInfoController(IptDbContext context)
        //{
        //    _context = context;
        //}

        // GET: PersonalInfo
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
                    return NotFound();
                }

                var personalInfo = await _context.PersonalInfo
                    .FirstOrDefaultAsync(m => m.FirstName == id);
                if (personalInfo == null)
                {
                    return NotFound();
                }
                return View(personalInfo);
            } 
            else
            {
                var personalInfo = await _context.PersonalInfo
                   .FirstOrDefaultAsync(m => m.UserId == id);

                return View(personalInfo);
            }    
        }
        //[HttpGet]
        //[Route("Users/current")]
        //public async Task<IActionResult> CurrentUser()
        //{
        //    string id = HttpContext.User.FindFirstValue("UserId");
        //    //ViewData["UserID"] = id;

        //    return Ok(new { UserId = id });
        //}
        // GET: PersonalInfo/Create
        //[HttpGet]
        //[Route("Users/current")]
        public IActionResult Create()
        {
            //string id = HttpContext.User.FindFirstValue(CurrentUser);
            //ViewData["UserID"] = id;
            ViewData["UserID"] = _userManager.GetUserId(this.User);
            return View();
        }

        // POST: PersonalInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonalId,UserId,FirstName,LastName,Section,Course,YearLevel,ContactNumber,DateOfBirth,Address,EmergencyContact")] PersonalInfo personalInfo)
        {
            //var CurrentUser = _userManager.GetUserId(this.User);
            if (ModelState.IsValid)
            {
                //personalInfo.UserId = CurrentUser;

                _context.Add(personalInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personalInfo);
        }

        // GET: PersonalInfo/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.PersonalInfo == null)
            {
                return NotFound();
            }

            var personalInfo = await _context.PersonalInfo.FindAsync(id);
            if (personalInfo == null)
            {
                return NotFound();
            }
            return View(personalInfo);
        }

        // POST: PersonalInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FirstName,LastName,Section,Course,YearLevel,Address,DateOfBirth,ContactNumber,EmergencyContact")] PersonalInfo personalInfo)
        {
            if (id != personalInfo.PersonalId)
            {
                return NotFound();
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
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
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
                .FirstOrDefaultAsync(m => m.FirstName == id);
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
          return (_context.PersonalInfo?.Any(e => e.FirstName == id)).GetValueOrDefault();
        }
    }
}
