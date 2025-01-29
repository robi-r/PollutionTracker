using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PollutionTracker.Data;
using PollutionTracker.Models;

namespace PollutionTracker.Controllers
{
    public class AlertThresholdsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlertThresholdsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AlertThresholds
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AlertThresholds.Include(a => a.Area);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AlertThresholds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alertThreshold = await _context.AlertThresholds
                .Include(a => a.Area)
                .FirstOrDefaultAsync(m => m.ThresholdID == id);
            if (alertThreshold == null)
            {
                return NotFound();
            }

            return View(alertThreshold);
        }

        // GET: AlertThresholds/Create
        public IActionResult Create()
        {
            ViewData["AreaID"] = new SelectList(_context.Areas, "AreaID", "AreaID");
            return View();
        }

        // POST: AlertThresholds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThresholdID,Parameter,ThresholdValue,SeverityLevel,CreatedAt,AreaID")] AlertThreshold alertThreshold)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alertThreshold);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AreaID"] = new SelectList(_context.Areas, "AreaID", "AreaID", alertThreshold.AreaID);
            return View(alertThreshold);
        }

        // GET: AlertThresholds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alertThreshold = await _context.AlertThresholds.FindAsync(id);
            if (alertThreshold == null)
            {
                return NotFound();
            }
            ViewData["AreaID"] = new SelectList(_context.Areas, "AreaID", "AreaID", alertThreshold.AreaID);
            return View(alertThreshold);
        }

        // POST: AlertThresholds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ThresholdID,Parameter,ThresholdValue,SeverityLevel,CreatedAt,AreaID")] AlertThreshold alertThreshold)
        {
            if (id != alertThreshold.ThresholdID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alertThreshold);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlertThresholdExists(alertThreshold.ThresholdID))
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
            ViewData["AreaID"] = new SelectList(_context.Areas, "AreaID", "AreaID", alertThreshold.AreaID);
            return View(alertThreshold);
        }

        // GET: AlertThresholds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alertThreshold = await _context.AlertThresholds
                .Include(a => a.Area)
                .FirstOrDefaultAsync(m => m.ThresholdID == id);
            if (alertThreshold == null)
            {
                return NotFound();
            }

            return View(alertThreshold);
        }

        // POST: AlertThresholds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var alertThreshold = await _context.AlertThresholds.FindAsync(id);
            if (alertThreshold != null)
            {
                _context.AlertThresholds.Remove(alertThreshold);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlertThresholdExists(int? id)
        {
            return _context.AlertThresholds.Any(e => e.ThresholdID == id);
        }
    }
}
