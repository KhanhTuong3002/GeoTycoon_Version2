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
        private readonly GeoTycoonDbcontext _context;

        public IndexModel(GeoTycoonDbcontext context)
        {
            _context = context;
        }

        public IList<Tracking> Tracking { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByUsername { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByTitle { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Trackings.Include(t => t.Question).AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                if (SearchByUsername)
                {
                    query = query.Where(t => t.UserName.Contains(SearchTerm));
                }

                if (SearchByTitle)
                {
                    query = query.Where(t => t.Question.Title.Contains(SearchTerm));
                }
            }

            var trackingEntries = await query.ToListAsync();

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

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            var trackingToDelete = await _context.Trackings.FindAsync(id);

            if (trackingToDelete == null)
            {
                return NotFound();
            }

            _context.Trackings.Remove(trackingToDelete);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
