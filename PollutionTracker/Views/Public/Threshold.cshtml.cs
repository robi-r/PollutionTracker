using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PollutionTracker.Data;
using PollutionTracker.Models;

namespace PollutionTracker.Views.Public
{
    public class ThresholdModel : PageModel
    {
        private readonly PollutionTracker.Data.ApplicationDbContext _context;

        public ThresholdModel(PollutionTracker.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<AlertThreshold> AlertThreshold { get;set; } = default!;

        public async Task OnGetAsync()
        {
            AlertThreshold = await _context.AlertThresholds
                .Include(a => a.Area).ToListAsync();
        }
    }
}
