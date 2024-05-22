using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entites;
using DataAccess;

namespace GeoClinet.Pages.Profile123
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.GeoTycoonDbcontext _context;

        public IndexModel(DataAccess.GeoTycoonDbcontext context)
        {
            _context = context;
        }

        public IList<Profile> Profile { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByEmail { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByFirstName { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchByLastName { get; set; }

        public async Task OnGetAsync()
        {
            var profiles = from p in _context.Profiles
                           select p;

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                if (SearchByEmail || SearchByFirstName || SearchByLastName)
                {
                    if (SearchByEmail)
                    {
                        profiles = profiles.Where(p => p.User.Email.Contains(SearchTerm));
                    }

                    if (SearchByFirstName)
                    {
                        profiles = profiles.Where(p => p.FirstName.Contains(SearchTerm));
                    }

                    if (SearchByLastName)
                    {
                        profiles = profiles.Where(p => p.LastName.Contains(SearchTerm));
                    }
                }
                else
                {
                    profiles = profiles.Where(p => p.User.Email.Contains(SearchTerm));
                }
            }

            Profile = await profiles
                .Include(p => p.User)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostBanAsync(string id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            profile.Isbanned = true;
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUnbanAsync(string id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            profile.Isbanned = false;
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
