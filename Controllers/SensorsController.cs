using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PollutionTracker.Data;

namespace PollutionTracker.Controllers
{
    public class SensorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SensorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sensors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sensors.Include(s => s.AlertThreshold).Include(s => s.Area);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sensors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensor = await _context.Sensors
                .Include(s => s.AlertThreshold)
                .Include(s => s.Area)
                .FirstOrDefaultAsync(m => m.SensorID == id);
            if (sensor == null)
            {
                return NotFound();
            }

            return View(sensor);
        }

        // GET: Sensors/Create
        public IActionResult Create()
        {
            ViewData["AlertThresholdID"] = new SelectList(_context.AlertThresholds, "ThresholdID", "ThresholdID");
            ViewData["AreaID"] = new SelectList(_context.Areas, "AreaID", "AreaID");
            return View();
        }

        // POST: Sensors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SensorID,SensorType,ModelNumber,AreaID,AlertThresholdID")] Sensor sensor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sensor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlertThresholdID"] = new SelectList(_context.AlertThresholds, "ThresholdID", "ThresholdID", sensor.AlertThresholdID);
            ViewData["AreaID"] = new SelectList(_context.Areas, "AreaID", "AreaID", sensor.AreaID);
            return View(sensor);
        }

        // GET: Sensors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensor = await _context.Sensors.FindAsync(id);
            if (sensor == null)
            {
                return NotFound();
            }
            ViewData["AlertThresholdID"] = new SelectList(_context.AlertThresholds, "ThresholdID", "ThresholdID", sensor.AlertThresholdID);
            ViewData["AreaID"] = new SelectList(_context.Areas, "AreaID", "AreaID", sensor.AreaID);
            return View(sensor);
        }

        // POST: Sensors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SensorID,SensorType,ModelNumber,AreaID,AlertThresholdID")] Sensor sensor)
        {
            if (id != sensor.SensorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sensor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SensorExists(sensor.SensorID))
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
            ViewData["AlertThresholdID"] = new SelectList(_context.AlertThresholds, "ThresholdID", "ThresholdID", sensor.AlertThresholdID);
            ViewData["AreaID"] = new SelectList(_context.Areas, "AreaID", "AreaID", sensor.AreaID);
            return View(sensor);
        }

        // GET: Sensors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensor = await _context.Sensors
                .Include(s => s.AlertThreshold)
                .Include(s => s.Area)
                .FirstOrDefaultAsync(m => m.SensorID == id);
            if (sensor == null)
            {
                return NotFound();
            }

            return View(sensor);
        }

        // POST: Sensors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sensor = await _context.Sensors.FindAsync(id);
            if (sensor != null)
            {
                _context.Sensors.Remove(sensor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SensorExists(int id)
        {
            return _context.Sensors.Any(e => e.SensorID == id);
        }
    }
}
