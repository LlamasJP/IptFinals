using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IptFinals.Data;
using IptFinals.Models;

namespace IptFinals.Views
{
    public class PersonalInfoes1Controller : Controller
    {
        private readonly IptDbContext _context;

        public PersonalInfoes1Controller(IptDbContext context)
        {
            _context = context;
        }

        // GET: PersonalInfoes1
        public async Task<IActionResult> Index()
        {
              return _context.PersonalInfo != null ? 
                          View(await _context.PersonalInfo.ToListAsync()) :
                          Problem("Entity set 'IptDbContext.PersonalInfo'  is null.");
        }

        // GET: PersonalInfoes1/Details/5
        public async Task<IActionResult> Details(string id)
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

        // GET: PersonalInfoes1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonalInfoes1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonalId,StudentId,UserId,FirstName,LastName,Section,Course,YearLevel,ContactNumber,DateOfBirth,Address,EmergencyContact")] PersonalInfo personalInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personalInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personalInfo);
        }

        // GET: PersonalInfoes1/Edit/5
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

        // POST: PersonalInfoes1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonalId,StudentId,UserId,FirstName,LastName,Section,Course,YearLevel,ContactNumber,DateOfBirth,Address,EmergencyContact")] PersonalInfo personalInfo)
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

        // GET: PersonalInfoes1/Delete/5
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

        // POST: PersonalInfoes1/Delete/5
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
