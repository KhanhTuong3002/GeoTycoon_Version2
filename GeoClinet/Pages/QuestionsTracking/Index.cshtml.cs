using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entites;
using DataAccess;

namespace GeoClinet.Pages.QuestionsTracking
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.GeoTycoonDbcontext _context;

        public IndexModel(DataAccess.GeoTycoonDbcontext context)
        {
            _context = context;
        }

        public IList<Tracking> Tracking { get;set; } = default!;

        public async Task OnGetAsync()
        {
           var trackingEntries = await _context.Trackings
                .Include(t => t.Question).ToListAsync();

            foreach (var tracking in trackingEntries)
            {
                var user = await _context.Users.FindAsync(tracking.UserId);
                if (user != null)
                {
                    tracking.UserName = user.UserName;
                }
            }

            Tracking = trackingEntries;

        }
    }
}
