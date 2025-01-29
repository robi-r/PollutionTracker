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
    public class PollutionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PollutionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pollutions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pollutions.Include(p => p.Sensor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pollutions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollution = await _context.Pollutions
                .Include(p => p.Sensor)
                .FirstOrDefaultAsync(m => m.PollutionID == id);
            if (pollution == null)
            {
                return NotFound();
            }

            return View(pollution);
        }

        // GET: Pollutions/Create
        public IActionResult Create()
        {
            ViewData["SensorID"] = new SelectList(_context.Sensors, "SensorID", "SensorID");
            return View();
        }

        // POST: Pollutions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PollutionID,SensorID,Temperature,Humidity,LPG_Isobutane,CarbonMonoxide,Hydrogen,CO2,NH3,RecordedAt")] Pollution pollution)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pollution);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SensorID"] = new SelectList(_context.Sensors, "SensorID", "SensorID", pollution.SensorID);
            return View(pollution);
        }

        // GET: Pollutions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollution = await _context.Pollutions.FindAsync(id);
            if (pollution == null)
            {
                return NotFound();
            }
            ViewData["SensorID"] = new SelectList(_context.Sensors, "SensorID", "SensorID", pollution.SensorID);
            return View(pollution);
        }

        // POST: Pollutions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PollutionID,SensorID,Temperature,Humidity,LPG_Isobutane,CarbonMonoxide,Hydrogen,CO2,NH3,RecordedAt")] Pollution pollution)
        {
            if (id != pollution.PollutionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pollution);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PollutionExists(pollution.PollutionID))
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
            ViewData["SensorID"] = new SelectList(_context.Sensors, "SensorID", "SensorID", pollution.SensorID);
            return View(pollution);
        }

        // GET: Pollutions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pollution = await _context.Pollutions
                .Include(p => p.Sensor)
                .FirstOrDefaultAsync(m => m.PollutionID == id);
            if (pollution == null)
            {
                return NotFound();
            }

            return View(pollution);
        }

        // POST: Pollutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pollution = await _context.Pollutions.FindAsync(id);
            if (pollution != null)
            {
                _context.Pollutions.Remove(pollution);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PollutionExists(int id)
        {
            return _context.Pollutions.Any(e => e.PollutionID == id);
        }
    }
}
