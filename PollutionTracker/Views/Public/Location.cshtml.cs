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
    public class LocationModel : PageModel
    {
        private readonly PollutionTracker.Data.ApplicationDbContext _context;

        public LocationModel(PollutionTracker.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Area> Area { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Area = await _context.Areas.ToListAsync();
        }
    }
}
