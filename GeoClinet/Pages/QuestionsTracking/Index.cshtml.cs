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
            // Default to SearchByUsername if no search type is selected
            if (!SearchByUsername && !SearchByTitle)
            {
                SearchByTitle = true;
            }

            var query = _context.Trackings
                        .Include(t => t.Question)
                        .AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                if (SearchByUsername && SearchByTitle)
                {
                    query = query.Where(t => t.UserName.Contains(SearchTerm) || t.Question.Title.Contains(SearchTerm));
                }
                else if (SearchByUsername)
                {
                    query = query.Where(t => t.UserName.Contains(SearchTerm));
                }
                else if (SearchByTitle)
                {
                    query = query.Where(t => t.Question.Title.Contains(SearchTerm));
                }
            }

            Tracking = await query.ToListAsync();

            // Populate UserName for each Tracking entry
            foreach (var tracking in Tracking)
            {
                var user = await _context.Users.FindAsync(tracking.UserId);
                if (user != null)
                {
                    tracking.UserName = user.UserName;
                }
            }
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
