using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PollutionTracker.Data;
using PollutionTracker.Models;
using System.Collections.Generic;
using System.Linq;

namespace PollutionTracker.Views.Home
{
    [Authorize]
    public class DashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // Properties to hold the data passed to the view
        public List<Area> Areas { get; set; }
        public List<AlertThreshold> AlertThresholds { get; set; }
        public List<Pollution> PollutionLevels { get; set; }
        public List<Sensor> Sensors { get; set; }

        public void OnGet()
        {
            // Fetch data from the database
            Areas = _context.Areas.ToList();
            AlertThresholds = _context.AlertThresholds.ToList();
            PollutionLevels = _context.Pollutions.ToList();
            Sensors = _context.Sensors.ToList();
        }
    }
}
